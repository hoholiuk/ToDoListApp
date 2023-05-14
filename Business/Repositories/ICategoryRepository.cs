using BusinessLogic.Models;

namespace BusinessLogic.Repositories
{
    public interface ICategoryRepository
    {
        RepositoryType RepositoryType { get; }

        IEnumerable<CategoryModel> GetAllCategories();

        CategoryModel GetById(int id);

        CategoryModel Create(CategoryModel category);

        void Delete(int id);
    }
}
