using Dapper;
using Matilha.Autentication.Domain.Interfaces.Repositories;
using Matilha.Autentication.Domain.Models.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Matilha.Autentication.Infrastructure.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly string _connectionString;

        public RefreshTokenRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task AddRefreshTokenAsync(RefreshToken refreshToken)
        {
            const string sql = @"
                INSERT INTO RefreshTokens (UserId, CompanyId, SessionId, Token, CreatedAt, ExpiresAt, IsValid)
                VALUES (@UserId, @CompanyId, @SessionId, @Token, @CreatedAt, @ExpiresAt, @IsValid);
                SELECT CAST(SCOPE_IDENTITY() as int);";

            using (var connection = new SqlConnection(_connectionString))
            {
                refreshToken.TokenId = await connection.QuerySingleAsync<int>(sql, refreshToken);
            }
        }

        public async Task<RefreshToken> GetRefreshTokenAsync(string token)
        {
            const string sql = "SELECT * FROM RefreshTokens WHERE Token = @Token AND IsValid = 1";

            using (var connection = new SqlConnection(_connectionString))
            {
                return await connection.QuerySingleOrDefaultAsync<RefreshToken>(sql, new { Token = token });
            }
        }

        public async Task InvalidateRefreshTokenAsync(string token)
        {
            const string sql = "UPDATE RefreshTokens SET IsValid = 0 WHERE Token = @Token";

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.ExecuteAsync(sql, new { Token = token });
            }
        }
    }
}
