using AutoMapper;
using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.AspNetCore.Mvc;
using WebApp.ViewModels;
using WebApp.ViewModels.Category;

namespace WebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IMapper mapper;
        private readonly ICategoryRepository categoryRepository;

        public CategoryController(IMapper mapper, ICategoryRepository categoryRepository)
        {
            this.mapper = mapper;
            this.categoryRepository = categoryRepository;
        }

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

            var categoryModel = mapper.Map<CategoryModel>(categoryInputViewModel);
            categoryRepository.Create(categoryModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            return RedirectToAction("Index");
        }

        private CategoryViewModel GetCategoryViewModel()
        {
            IEnumerable<CategoryModel> categoriesList = categoryRepository.GetCategoriesList();

            return new CategoryViewModel()
            {
                CategoriesList = mapper.Map<List<CategoryItemViewModel>>(categoriesList)
            };
        }
    }
}
