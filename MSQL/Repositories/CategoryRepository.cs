using Dapper;
using Microsoft.Data.SqlClient;
using BusinessLogic.Models;
using BusinessLogic.Repositories;

namespace MSQL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private string connectionString = "Data Source=HBV; Initial Catalog=ToDoList; Integrated Security=True; TrustServerCertificate=True";

        public IEnumerable<CategoryModel> GetCategoriesList()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<CategoryModel>("SELECT * FROM Categories").ToList();
            }
        }

        public int Create(CategoryModel category)
        {
            string query = @"
                INSERT INTO Categories (Name)
                VALUES (@Name)
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(query, category);
            }
        }

        public int Delete(int id)
        {
            string query = @"
                UPDATE Tasks
                SET CategoryId = null
                WHERE CategoryId = @Id

                DELETE Categories
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Execute(query, new { Id = id });
            }
        }
    }
}
