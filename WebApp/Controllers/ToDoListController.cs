using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic.Models;
using WebApp.ViewModels.Task;
using WebApp.ViewModels.Category;
using WebApp.ViewModels;
using BusinessLogic.Repositories;

namespace WebApp.Controllers
{
    public class ToDoListController : Controller
    {
        private readonly IMapper mapper;
        private readonly ITaskRepository taskRepository;
        private readonly ICategoryRepository categoryRepository;

        public ToDoListController(IMapper mapper, ITaskRepository taskRepository, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.taskRepository = taskRepository;
            this.categoryRepository = categoryRepository;
        }

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
            
            var taskModel = mapper.Map<TaskModel>(taskInputViewModel);
            taskRepository.Create(taskModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Complete(int id)
        {
            taskRepository.Complete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            taskRepository.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(int id)
        {
            TaskModel task = taskRepository.GetById(id);

            if(task == null)
            {
                return RedirectToAction("Index");
            }

            TaskInputViewModel taskInputViewModel = new TaskInputViewModel()
            {
                Title = task.Title,
                DueDate = task.DueDate,
                CategoryId = task.CategoryId
            };

            ToDoListViewModel toDoListViewModel = GetToDoListViewModel();
            toDoListViewModel.TaskInputViewModel = taskInputViewModel;

            taskRepository.Delete(id);
            return View("Index", toDoListViewModel);
        }

        private ToDoListViewModel GetToDoListViewModel()
        {
            IEnumerable<TaskModel> tasksList = taskRepository.GetTasksList();
            IEnumerable<CategoryModel> categoriesList = categoryRepository.GetCategoriesList();

            return new ToDoListViewModel()
            {
                TasksList = mapper.Map<List<TaskItemViewModel>>(tasksList),
                CategoriesList = mapper.Map<List<CategoryItemViewModel>>(categoriesList)
            };
        }
    }
}
