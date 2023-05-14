using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApp.Services;
using WebApp.ViewModels;
using WebApp.ViewModels.Category;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper _mapper;
        private ICategoryRepository _categoryRepository { get => _repositorySwitcher.GetCategoryRepository(Request); }
        private readonly RepositorySwitcher _repositorySwitcher;

        public CategoryController(IMapper mapper, IEnumerable<ICategoryRepository> categoryRepositories)
        {
            _mapper = mapper;
            _repositorySwitcher = new RepositorySwitcher(categoryRepositories);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(GetCategoryViewModel());
        }

        [HttpPost]
        public IActionResult Index(CategoryInputViewModel categoryInputViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(GetCategoryViewModel());
            }

            var categoryModel = _mapper.Map<CategoryModel>(categoryInputViewModel);
            _categoryRepository.Create(categoryModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            _categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private CategoryViewModel GetCategoryViewModel()
        {
            IEnumerable<CategoryModel> categoriesList = _categoryRepository.GetAllCategories();

            return new CategoryViewModel()
            {
                CategoriesList = _mapper.Map<List<CategoryItemViewModel>>(categoriesList)
            };
        }
    }
}
