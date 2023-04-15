using AutoMapper;
using BusinessLogic.Models;
using WebApp.ViewModels.Task;
using WebApp.ViewModels.Category;

namespace WebApp
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<TaskModel, TaskItemViewModel>();
            CreateMap<CategoryModel, CategoryItemViewModel>();
            CreateMap<TaskInputViewModel, TaskModel>();
            CreateMap<CategoryInputViewModel, CategoryModel>();
        }
    }
}
