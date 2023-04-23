using BusinessLogic.Models;

namespace BusinessLogic.Repositories
{
    public interface ICategoryRepository
    {
        RepositoryType RepositoryType { get; }

        IEnumerable<CategoryModel> GetCategoriesList();

        void Create(CategoryModel category);

        void Delete(int id);
    }
}
