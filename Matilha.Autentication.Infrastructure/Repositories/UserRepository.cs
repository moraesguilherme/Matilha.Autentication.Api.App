using Dapper;
using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Matilha.Autentication.Domain.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<int> AddUserAsync(User user)
        {
            const string sql = @"
                INSERT INTO Users (Username, PasswordHash, Email, FullName, PhoneNumber, CompanyId)
                VALUES (@Username, @PasswordHash, @Email, @FullName, @PhoneNumber, @CompanyId);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleAsync<int>(sql, user);
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            const string sql = "SELECT * FROM Users WHERE Username = @Username";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
            }
        }

        public async Task<Company> AddCompanyAsync(string companyName)
        {
            const string sql = @"
                INSERT INTO Companies (CompanyName)
                VALUES (@CompanyName);
                SELECT CAST(SCOPE_IDENTITY() as int)";

            using (var connection = new SqlConnection(_connectionString))
            {
                int companyId = await connection.QuerySingleAsync<int>(sql, new { CompanyName = companyName });
                return new Company { CompanyId = companyId, CompanyName = companyName };
            }
        }

        public async Task<Company> GetCompanyByNameAsync(string companyName)
        {
            const string sql = "SELECT * FROM Companies WHERE CompanyName = @CompanyName";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Company>(sql, new { CompanyName = companyName });
            }
        }
    }
}
