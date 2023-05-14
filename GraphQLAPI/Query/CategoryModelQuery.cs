using BusinessLogic.Repositories;
using GraphQL;
using GraphQL.Types;
using GraphQLAPI.Services;
using GraphQLAPI.Type;

namespace GraphQLAPI.Query
{
    public class CategoryModelQuery : ObjectGraphType
    {
        public CategoryModelQuery(IEnumerable<ICategoryRepository> categoryRepositories, IHttpContextAccessor accessor)
        {
            var _repositorySwitcher = new RepositorySwitcher(categoryRepositories);
            ICategoryRepository categoryRepository = _repositorySwitcher.GetCategoryRepository(accessor);

            Field<ListGraphType<CategoryModelType>>("categories",
                resolve: context =>
                {
                    return categoryRepository.GetAllCategories();
                });

            Field<CategoryModelType>("category",
                arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "id" }),
                resolve: context =>
                {
                    return categoryRepository.GetById(context.GetArgument<int>("id"));
                });
        }
    }
}
