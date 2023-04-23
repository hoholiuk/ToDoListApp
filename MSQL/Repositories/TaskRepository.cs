using Dapper;
using Microsoft.Data.SqlClient;
using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;

namespace MSQL.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public RepositoryType RepositoryType { get => RepositoryType.SQL; }
        private string connectionString { get; set; }

        public TaskRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MSQLConnection");
        }

        public TaskModel GetById(int id)
        {
            string query = @"
                SELECT *
                FROM Tasks
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.QuerySingleOrDefault<TaskModel>(query, new { Id = id });
            }
        }

        public IEnumerable<TaskModel> GetTasksList()
        {
            string query = @"
                SELECT * FROM Tasks
                ORDER BY CASE
	                WHEN DueDate IS NULL THEN 1
	                ELSE 0
	                END,
	                DueDate
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<TaskModel>(query).ToList();
            }
        }

        public void Create(TaskModel task)
        {
            string query = @"
                INSERT INTO Tasks (Title, DueDate, CategoryId)
                VALUES (@Title, @DueDate, @CategoryId)
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, task);
            }
        }

        public void Update(TaskModel task)
        {
            string query = @"
                UPDATE Tasks
                SET Title = @Title,
                DueDate = @DueDate,
                CategoryId = @CategoryId
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, task);
            }
        }

        public void Complete(int id)
        {
            string query = @"
                UPDATE Tasks
                SET IsCompleted = IsCompleted ^ 1
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { Id = id });
            }
        }

        public void Delete(int id)
        {
            string query = @"
                DELETE Tasks
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, new { Id = id });
            }
        }
    }
}
