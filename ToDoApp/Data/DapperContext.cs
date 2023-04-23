using Microsoft.Data.SqlClient;
using System.Data;
using ToDoApp.Services;

namespace ToDoApp.Services
{
    public class DapperContext
    {
        private readonly string _connectionString;
        private readonly string _storagePath;

        public DapperContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
            _storagePath = config.GetValue<string>("XmlStoragePath");
        }

        public SqlConnection GetConnection() => new SqlConnection(_connectionString);
        

        public string GetStoragePath() => $"{Environment.CurrentDirectory}{_storagePath}";
        
    }
}