using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;
using ToDoApp.Interfaces;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Repository
{
    public class TaskRepository : ITaskRepository
    {
        private readonly DapperContext _context;
        public TaskRepository(DapperContext context)
        {
            _context = context;
        }

        public List<TaskDto> Get()
        {
            List<TaskDto> data = new List<TaskDto>();
            string query = "SELECT * FROM Task JOIN Category ON Task.CategoryId = Category.Id " +
                            "ORDER BY Task.IsCompleted ASC, Task.CategoryId";

            using (SqlConnection connection = _context.GetConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) 
                {
                    TaskDto dto = new TaskDto() 
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        Name = Convert.ToString(reader["Name"]),
                        Deadline = Convert.ToDateTime(reader["Deadline"]),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                    };

                    data.Add(dto);
                }
                connection.Close();
            }

            return data.ToList();
        }

        public List<TaskDto> GetByCategory(int categoryId) 
        {
            List<TaskDto> data = new List<TaskDto>();
            string query = "SELECT * FROM Task JOIN Category ON Task.CategoryId = Category.Id " +
                            "WHERE CategoryId = @CategoryId " +
                            "ORDER BY Task.IsCompleted ASC, Task.CategoryId";

            using (SqlConnection connection = _context.GetConnection())
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("CategoryId", categoryId);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TaskDto dto = new TaskDto()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        Name = Convert.ToString(reader["Name"]),
                        Deadline = Convert.ToDateTime(reader["Deadline"]),
                        IsCompleted = Convert.ToBoolean(reader["IsCompleted"])
                    };

                    data.Add(dto);
                }
                connection.Close();
            }

            return data.ToList();
        }
        public void Add(TaskDto task) 
        {
            string query = "INSERT INTO Task (CategoryId, Name, Deadline, IsCompleted) " +
                            "VALUES (@CategoryId, @Name, @Deadline, @IsCompleted)";

            using (SqlConnection connection = _context.GetConnection())
            {
                    connection.Open();

                    SqlCommand cmd = new SqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("CategoryId", task.CategoryId);
                    cmd.Parameters.AddWithValue("Name", task.Name);
                    cmd.Parameters.AddWithValue("Deadline", task.Deadline);
                    cmd.Parameters.AddWithValue("IsCompleted", task.IsCompleted);

                    cmd.ExecuteNonQuery();
                    connection.Close();
            }
        }

        public void Delete(int id)
        {
            string query = "DELETE FROM Task WHERE Id = @Id";

            using (SqlConnection connection = _context.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }

        public void ChangeCompletedState(int id, bool state) 
        {
            string query = "UPDATE Task SET IsCompleted = @IsCompleted WHERE Id = @Id";

            using (SqlConnection connection = _context.GetConnection())
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.AddWithValue("IsCompleted", state);
                cmd.Parameters.AddWithValue("Id", id);

                cmd.ExecuteNonQuery();
                connection.Close();
            }
        }
    }
}
