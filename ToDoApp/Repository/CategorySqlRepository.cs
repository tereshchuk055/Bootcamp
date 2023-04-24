using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Repository
{
    public class CategorySqlRepository : ICategoryRepository
    {
        private readonly DbContext _context;
        public CategorySqlRepository(DbContext context)
        {
            _context = context;
        }
        public void Add(CategoryDto category)
        {
            string query = "INSERT INTO Category (Name) VALUES (@Name)";

            using (SqlConnection connection = _context.GetConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("Name", category.Name);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Category WHERE Id = @Id";

            using (SqlConnection connection = _context.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public List<CategoryDto> Get()
        {
            List<CategoryDto> data = new List<CategoryDto>();
            string query = "SELECT * FROM Category";

            using (SqlConnection connection = _context.GetConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    CategoryDto dto = new CategoryDto()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Name = Convert.ToString(reader["Name"])
                    };

                    data.Add(dto);
                }

                connection.Close();
            }

            return data.ToList();
        }
    }
}
