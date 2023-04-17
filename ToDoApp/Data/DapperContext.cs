using Microsoft.Data.SqlClient;
using System.Data;
using ToDoApp.Services;

namespace ToDoApp.Services
{
    public class DapperContext
    {
        private readonly string _connectionString;

        public DapperContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public SqlConnection GetConnection() => new SqlConnection(_connectionString);
        
    }
}