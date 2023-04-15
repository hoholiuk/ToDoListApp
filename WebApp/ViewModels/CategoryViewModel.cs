using WebApp.ViewModels.Category;

namespace WebApp.ViewModels
{
    public class CategoryViewModel
    {
        public List<CategoryItemViewModel> CategoriesList { get; set; } = new List<CategoryItemViewModel>();

        public CategoryInputViewModel CategoryInputViewModel { get; set; } = new CategoryInputViewModel();
    }
}
