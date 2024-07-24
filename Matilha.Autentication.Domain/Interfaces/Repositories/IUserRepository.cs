using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
    }
}
