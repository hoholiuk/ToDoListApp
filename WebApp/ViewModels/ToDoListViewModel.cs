using WebApp.ViewModels.Task;
using WebApp.ViewModels.Category;

namespace WebApp.ViewModels
{
    public class ToDoListViewModel
    {
        public List<TaskItemViewModel> TasksList { get; set; } = new List<TaskItemViewModel>();
        
        public List<CategoryItemViewModel> CategoriesList { get; set; } = new List<CategoryItemViewModel>();

        public TaskInputViewModel TaskInputViewModel { get; set; } = new TaskInputViewModel();
    }
}
