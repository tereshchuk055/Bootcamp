using Microsoft.Data.SqlClient;
using System.Data;
using ToDoApp.Services;

namespace ToDoApp.Services
{
    public class DbContext
    {
        private readonly string _connectionString;
        private readonly string _storagePath;

        public DbContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection") ?? throw new Exception("Failed to connect to Database");
            _storagePath = config.GetValue<string>("XmlStoragePath") ?? throw new Exception("Failed to connect to Database");
        }

        public SqlConnection GetConnection() => new SqlConnection(_connectionString);
        

        public string GetStoragePath() => $"{Environment.CurrentDirectory}{_storagePath}";
        
    }
}