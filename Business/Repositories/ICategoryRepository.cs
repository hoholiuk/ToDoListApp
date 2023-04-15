using BusinessLogic.Models;

namespace BusinessLogic.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<CategoryModel> GetCategoriesList();
        int Create(CategoryModel category);
        int Delete(int id);
    }
}
