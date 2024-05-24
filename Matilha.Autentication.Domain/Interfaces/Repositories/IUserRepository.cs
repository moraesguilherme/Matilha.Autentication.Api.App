using Matilha.Autentication.Domain.Models.Entities;

namespace Matilha.Autentication.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<int> AddUserAsync(User user);
        Task<User> GetUserByUsernameAsync(string username);
        Task<Company> AddCompanyAsync(string companyName);
        Task<Company> GetCompanyByNameAsync(string companyName);
    }
}
