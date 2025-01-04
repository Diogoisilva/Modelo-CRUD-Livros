using System;
using System.Data;
using System.Data.SqlClient;


namespace Modelo.Cross.Helpers
{
    public static class DbConnectionHelper
    {
        private static string _connectionString = "Mudar isso depois quando começar a conectar, puxar a connection string do appsetting";

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
