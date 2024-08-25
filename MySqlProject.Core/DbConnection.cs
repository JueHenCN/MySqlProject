using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlProject.Core
{
    public class DbConnection
    {
        private readonly DatabaseConfig config;
        private MySqlConnection connection;

        public DbConnection(DatabaseConfig config)
        {
            this.config = config;
        }

        public MySqlConnection MySqlConn => connection;

        public async Task<bool> OpenAsync()
        {
            connection = new MySqlConnection(config.GetConnectionString());
            try
            {
                await connection.OpenAsync();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task CloseConnectionAsync(MySqlConnection connection)
        {
            try
            {
                await connection.CloseAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    public class DatabaseConfig
    {
        public string Server { get; set; }
        public int Port { get; set; } = 3306;  // 默认端口为3306
        public string Database { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }

        public string GetConnectionString()
        {
            return $"Server={Server};Port={Port};Database={Database};User ID={UserId};Password={Password};";
        }
    }
}
