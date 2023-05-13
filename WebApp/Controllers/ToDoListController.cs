using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;
using WebApp.ViewModels.Category;
using WebApp.ViewModels.Task;

namespace WebApp.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly IMapper _mapper;
        private ITaskRepository _taskRepository { get => _repositorySwitcher.GetTaskRepository(Request); }
        private ICategoryRepository _categoryRepository { get => _repositorySwitcher.GetCategoryRepository(Request); }
        private readonly RepositorySwitcher _repositorySwitcher;

        public ToDoListController(IMapper mapper, IEnumerable<ITaskRepository> taskRepositories, IEnumerable<ICategoryRepository> categoryRepositories)
        {
            _mapper = mapper;
            _repositorySwitcher = new RepositorySwitcher(taskRepositories, categoryRepositories);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetToDoListViewModel());
        }

        [HttpPost]
        public IActionResult Index(TaskInputViewModel taskInputViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(GetToDoListViewModel());
            }
            
            var taskModel = _mapper.Map<TaskModel>(taskInputViewModel);
            _taskRepository.Create(taskModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Complete(int id)
        {
            _taskRepository.Complete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _taskRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult GetDataForUpdate(int id)
        {
            TaskModel task = _taskRepository.GetById(id);

            if(task == null)
            {
                return RedirectToAction("Index");
            }

            TaskInputViewModel taskInputViewModel = new TaskInputViewModel()
            {
                Id = id,
                Title = task.Title,
                DueDate = task.DueDate,
                CategoryId = task.CategoryId
            };

            ToDoListViewModel toDoListViewModel = GetToDoListViewModel();
            toDoListViewModel.TaskInputViewModel = taskInputViewModel;

            return View("Index", toDoListViewModel);
        }

        [HttpPost]
        public IActionResult Update(TaskInputViewModel taskInputViewModel)
        {
            if (!ModelState.IsValid)
            {
                ToDoListViewModel toDoListViewModel = GetToDoListViewModel();
                toDoListViewModel.TaskInputViewModel = taskInputViewModel;
                return View("Index", toDoListViewModel);
            }

            var taskModel = _mapper.Map<TaskModel>(taskInputViewModel);
            _taskRepository.Update(taskModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeRepository(RepositoryType repository)
        {
            Response.Cookies.Append("repositoryType", repository.ToString());
            return RedirectToAction("Index");
        }

        private ToDoListViewModel GetToDoListViewModel()
        {
            IEnumerable<TaskModel> tasksList = _taskRepository.GetTasksList();
            IEnumerable<CategoryModel> categoriesList = _categoryRepository.GetCategoriesList();

            return new ToDoListViewModel()
            {
                TasksList = _mapper.Map<List<TaskItemViewModel>>(tasksList),
                CategoriesList = _mapper.Map<List<CategoryItemViewModel>>(categoriesList)
            };
        }
    }
}
