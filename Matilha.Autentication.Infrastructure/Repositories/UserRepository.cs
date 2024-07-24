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

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            const string sql = "SELECT * FROM Users WHERE Username = @Username";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
            }
        }
    }
}
