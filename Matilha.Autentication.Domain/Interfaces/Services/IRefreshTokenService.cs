using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Interfaces.Services
{
    public interface IRefreshTokenService
    {
        Task<RefreshToken> GenerateRefreshTokenAsync(int userId);
        Task<RefreshToken> GetRefreshTokenAsync(string token);
        Task InvalidateRefreshTokenAsync(string token);
        Task<bool> ValidateRefreshTokenAsync(string token);
    }
}
