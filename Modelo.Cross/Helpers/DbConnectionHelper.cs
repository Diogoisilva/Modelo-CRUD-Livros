using System.Data.SqlClient;
using System.Data;
using Microsoft.Extensions.Configuration;

namespace Modelo.Cross.Helpers
{
    public static class DbConnectionHelper
    {
        private static string _connectionString;

        static DbConnectionHelper()
        {
            // Configurações do App
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Define o caminho base
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Lê o arquivo de configurações
                .Build();

            // Obtém a string de conexão do arquivo de configuração
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        public static void OpenConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Open)
                connection.Open();
        }

        public static void CloseConnection(SqlConnection connection)
        {
            if (connection.State != ConnectionState.Closed)
                connection.Close();
        }
    }
}
