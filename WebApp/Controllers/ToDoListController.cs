﻿using AutoMapper;
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

        public ToDoListController(IMapper mapper, IEnumerable<ITaskRepository> taskRepository, IEnumerable<ICategoryRepository> categoryRepository)
        {
            this.mapper = mapper;
            this.taskRepository = taskRepository.FirstOrDefault(r => r.RepositoryType == CurrentRepository.repositoryType);
            this.categoryRepository = categoryRepository.FirstOrDefault(r => r.RepositoryType == CurrentRepository.repositoryType);
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
        public IActionResult GetDataForUpdate(int id)
        {
            TaskModel task = taskRepository.GetById(id);

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

            var taskModel = mapper.Map<TaskModel>(taskInputViewModel);
            taskRepository.Update(taskModel);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult ChangeRepository(RepositoryType repository)
        {
            CurrentRepository.ChangeRepositoryType(repository);
            return RedirectToAction("Index");
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
