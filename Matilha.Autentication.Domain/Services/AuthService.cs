using Matilha.Autentication.Domain.Models.Entities;
using Matilha.Autentication.Domain.Models.DTOs;
using Matilha.Autentication.Domain.Repositories;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;
using Matilha.Autentication.Domain.Interfaces.Services;

namespace Matilha.Autentication.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly IUserRepository _userRepository;

        public AuthService(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
                return null;

            return GenerateJwtToken(user);
        }

        public async Task<User> RegisterAsync(RegisterUser registerUser)
        {
            if (await _userRepository.GetUserByUsernameAsync(registerUser.Username) != null)
                return null;

            var company = await _userRepository.GetCompanyByNameAsync(registerUser.CompanyName);
            if (company == null)
            {
                company = await _userRepository.AddCompanyAsync(registerUser.CompanyName);
            }

            var user = new User
            {
                Username = registerUser.Username,
                PasswordHash = HashPassword(registerUser.Password),
                Email = registerUser.Email,
                FullName = registerUser.FullName,
                PhoneNumber = registerUser.PhoneNumber,
                CompanyId = company.CompanyId
            };

            user.UserId = await _userRepository.AddUserAsync(user);

            return user;
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
                //Expires = DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpireMinutes"])),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = jwtSettings["Issuer"],
                Audience = jwtSettings["Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string HashPassword(string password)
        {
            // Implementar hashing de senha aqui
            return password; // Substitua com um hash real
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            // Implementar verificação de hash de senha aqui
            return password == storedHash; // Substitua com uma verificação real
        }
    }
}
