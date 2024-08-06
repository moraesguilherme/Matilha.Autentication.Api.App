using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Interfaces.Services;
using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Net.Http;
using Newtonsoft.Json;
using Matilha.Autentication.Domain.Repositories;

namespace Matilha.Autentication.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly ISessionService _sessionService;
        private readonly IUserPreferencesService _userPreferencesService;
        private readonly HttpClient _httpClient;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IConfiguration config, IUserRepository userRepository,
            IRefreshTokenService refreshTokenService, ISessionService sessionService, IUserPreferencesService userPreferencesService,
            HttpClient httpClient, ILogger<AuthService> logger)
        {
            _config = config;
            _userRepository = userRepository;
            _refreshTokenService = refreshTokenService;
            _sessionService = sessionService;
            _userPreferencesService = userPreferencesService;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<AuthenticateResult> AuthenticateAsync(string username, string password)
        {
            _logger.LogInformation(LogMessages.Messages["AuthenticateUserAttempt"], username);

            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                _logger.LogWarning(LogMessages.Messages["AuthenticateUserFailed"], username);
                return null;
            }

            var token = GenerateJwtToken(user);

            var session = new Session
            {
                UserId = user.UserId,
                CompanyId = user.CompanyId,
                Token = token,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddHours(12),
                IsValid = true
            };

            _logger.LogInformation(LogMessages.Messages["GenerateSessionAttempt"], user.UserId);
            var sessionId = await _sessionService.CreateSessionAsync(session);
            _logger.LogInformation(LogMessages.Messages["GenerateSessionSuccessful"], user.UserId);

            _logger.LogInformation(LogMessages.Messages["GenerateRefreshTokenForSessionAttempt"], sessionId);
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(user.UserId, user.CompanyId, sessionId);
            _logger.LogInformation(LogMessages.Messages["GenerateRefreshTokenForSessionSuccessful"], sessionId);

            var preferences = await _userPreferencesService.GetUserPreferencesAsync(user.UserId, token);
            if (preferences != null)
            {
                _logger.LogInformation(LogMessages.Messages["UserPreferencesFetched"], user.UserId);
            }

            _logger.LogInformation(LogMessages.Messages["AuthenticateUserSuccessful"], username);

            return new AuthenticateResult
            {
                Token = token,
                RefreshToken = refreshToken.Token,
                UserId = user.UserId,
                SessionId = sessionId,
                UserPreferences = preferences
            };
        }

        private string GenerateJwtToken(User user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = Encoding.ASCII.GetBytes(jwtSettings["Key"]);
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim("UserId", user.UserId.ToString()),
                    new Claim("CompanyId", user.CompanyId.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            _logger.LogInformation(LogMessages.Messages["GenerateJwtToken"], user.UserId);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return $"{Convert.ToBase64String(salt)}:{hashed}";
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var parts = storedHash.Split(':');
            if (parts.Length != 2)
            {
                throw new FormatException("Unexpected hash format. " +
                    "Should be formatted as '{salt}:{hash}'");
            }

            var salt = Convert.FromBase64String(parts[0]);
            var storedPasswordHash = parts[1];

            var hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));

            return hash == storedPasswordHash;
        }
    }
}
