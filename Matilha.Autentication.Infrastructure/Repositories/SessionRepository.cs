using Dapper;
using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Matilha.Autentication.Domain.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly string _connectionString;

        public SessionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddSessionAsync(Session session)
        {
            const string sql = @"
                INSERT INTO Sessions (UserId, Token, CreatedAt, ExpiresAt, IsValid)
                VALUES (@UserId, @Token, @CreatedAt, @ExpiresAt, @IsValid)";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, session);
            }
        }

        public async Task<Session> GetSessionAsync(string sessionId)
        {
            const string sql = "SELECT * FROM Sessions WHERE SessionId = @SessionId AND IsValid = 1";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<Session>(sql, new { SessionId = sessionId });
            }
        }

        public async Task InvalidateSessionAsync(string sessionId)
        {
            const string sql = "UPDATE Sessions SET IsValid = 0 WHERE SessionId = @SessionId";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { SessionId = sessionId });
            }
        }
    }
}
