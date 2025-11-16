using MySql.Data.MySqlClient;

namespace ControleFinanceiro.Database
{
    public static class MySqlConnectionFactory
    {
        private static readonly string connectionString =
            "Server=localhost;Database=orcamento_db;Uid=root;Pwd=12345678;";

        public static MySqlConnection GetConnection()
        {
            return new MySqlConnection(connectionString);
        }
    }
}
