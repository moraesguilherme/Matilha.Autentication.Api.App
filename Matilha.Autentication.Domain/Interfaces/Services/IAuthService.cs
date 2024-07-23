using Matilha.Autentication.Domain.Models.DTOs;
using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<AuthenticateResult> AuthenticateAsync(string username, string password);
        Task<User> RegisterAsync(RegisterUser registerUser);
    }
}
