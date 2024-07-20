using Dapper;
using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Matilha.Autentication.Infrastructure.Repositories
{
    public class AccessLogRepository : IAccessLogRepository
    {
        private readonly string _connectionString;

        public AccessLogRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddAccessLogAsync(AccessLog log)
        {
            const string sql = @"
                INSERT INTO AccessLogs (UserId, Action, Timestamp)
                VALUES (@UserId, @Action, @Timestamp)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, log);
            }
        }
    }
}
