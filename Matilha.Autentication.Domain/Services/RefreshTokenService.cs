using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Interfaces.Services;
using Matilha.Autentication.Domain.Models.Entities;
using System.ComponentModel.Design;

namespace Matilha.Autentication.Domain.Services
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenService(IRefreshTokenRepository refreshTokenRepository)
        {
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<RefreshToken> GenerateRefreshTokenAsync(int userId, int companyId, int sessionId)
        {
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                CompanyId = companyId,
                SessionId = sessionId,
                Token = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                IsValid = true
            };

            await _refreshTokenRepository.AddRefreshTokenAsync(refreshToken);
            return refreshToken;
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string token)
        {
            return await _refreshTokenRepository.GetRefreshTokenAsync(token);
        }

        public async Task InvalidateRefreshTokenAsync(string token)
        {
            await _refreshTokenRepository.InvalidateRefreshTokenAsync(token);
        }

        public async Task<bool> ValidateRefreshTokenAsync(string token)
        {
            var refreshToken = await _refreshTokenRepository.GetRefreshTokenAsync(token);
            return refreshToken != null && refreshToken.ExpiresAt > DateTime.UtcNow;
        }
    }
}
