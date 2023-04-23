using Dapper;
using Microsoft.Data.SqlClient;
using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;

namespace MSQL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public RepositoryType RepositoryType { get => RepositoryType.SQL; }
        private string connectionString { get; set; }

        public CategoryRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("MSQLConnection");
        }

        public IEnumerable<CategoryModel> GetCategoriesList()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                return connection.Query<CategoryModel>("SELECT * FROM Categories").ToList();
            }
        }

        public void Create(CategoryModel category)
        {
            string query = @"
                INSERT INTO Categories (Name)
                VALUES (@Name)
            ";

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Execute(query, category);
            }
        }

        public void Delete(int id)
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
                connection.Execute(query, new { Id = id });
            }
        }
    }
}
