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
        private string _connectionString { get; set; }

        public CategoryRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("MSQLConnection");
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<CategoryModel>("SELECT * FROM Categories").ToList();
            }
        }

        public CategoryModel GetById(int id)
        {
            string query = @"
                SELECT *
                FROM Categories
                WHERE Id = @Id
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QuerySingleOrDefault<CategoryModel>(query, new { Id = id });
            }
        }

        public CategoryModel Create(CategoryModel category)
        {
            string query = @"
                INSERT INTO Categories (Name)
                VALUES (@Name)
                SELECT SCOPE_IDENTITY()
            ";

            using (var connection = new SqlConnection(_connectionString))
            {
                var id = connection.ExecuteScalar<int>(query, category);
                category.Id = id;
                return category;
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

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute(query, new { Id = id });
            }
        }
    }
}
