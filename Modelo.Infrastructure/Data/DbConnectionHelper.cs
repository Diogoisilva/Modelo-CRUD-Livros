using System.Data.SqlClient;

namespace Modelo.Infrastructure.Data
{
    public class DbConnectionHelper
    {
        private readonly string _connectionString;

        public DbConnectionHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
