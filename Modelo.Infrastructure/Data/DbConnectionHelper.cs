using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Modelo.Infrastructure.Data
{
    public class DbConnectionHelper
    {
        private readonly IConfiguration _configuration;

        public DbConnectionHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
