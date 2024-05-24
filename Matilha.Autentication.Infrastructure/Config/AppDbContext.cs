using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace Matilha.Autentication.Infrastructure.Config
{
    public class AppDbContext
    {
        private readonly IConfiguration _config;
        public IDbConnection Connection { get; }

        public AppDbContext(IConfiguration config)
        {
            _config = config;
            string connectionString = _config.GetConnectionString("DefaultConnection");
            Connection = new SqlConnection(connectionString);
        }
    }
}
