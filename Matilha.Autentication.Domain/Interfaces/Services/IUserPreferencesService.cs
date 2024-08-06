using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Interfaces.Services
{
    public interface IUserPreferencesService
    {
        Task<List<UserPreferences>> GetUserPreferencesAsync(int userId, string token);
    }
}
