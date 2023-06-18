using BusinessLogic.Models;
using BusinessLogic.Repositories;
using GraphQL;
using GraphQL.Types;
using GraphQLAPI.Services;
using GraphQLAPI.Type;

namespace GraphQLAPI.Mutation
{
    public class CategoryModelMutation : ObjectGraphType
    {
        public CategoryModelMutation(IEnumerable<ICategoryRepository> categoryRepositories, IHttpContextAccessor accessor)
        {
            var _repositorySwitcher = new RepositorySwitcher(categoryRepositories);
            ICategoryRepository categoryRepository = _repositorySwitcher.GetCategoryRepository(accessor);

            Field<CategoryModelType>("createCategory",
                arguments: new QueryArguments(new QueryArgument<CategoryModelInputType> { Name = "category" }),
                resolve: context =>
                {
                    return categoryRepository.Create(context.GetArgument<CategoryModel>("category"));
                });

            Field<IntGraphType>("deleteCategory",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    var categoryId = context.GetArgument<int>("id");
                    categoryRepository.Delete(categoryId);
                    return categoryId;
                });
        }
    }
}
