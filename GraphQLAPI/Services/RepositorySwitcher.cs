using BusinessLogic.Repositories;
using GraphQL.Reflection;
using static System.Net.Mime.MediaTypeNames;

namespace GraphQLAPI.Services
{
    public class RepositorySwitcher
    {
        private readonly RepositoryType _defaultRepositoryType = RepositoryType.SQL;
        private readonly IEnumerable<ITaskRepository> _taskRepositories;
        private readonly IEnumerable<ICategoryRepository> _categoryRepositories;

        public RepositorySwitcher(IEnumerable<ICategoryRepository> categoryRepositories)
        {
            _categoryRepositories = categoryRepositories;
        }

        public RepositorySwitcher(IEnumerable<ITaskRepository> taskRepositories)
        {
            _taskRepositories = taskRepositories;
        }

        public ITaskRepository GetTaskRepository(IHttpContextAccessor accessor)
        {
            return _taskRepositories.FirstOrDefault(r => r.RepositoryType == GetRepositoryType(accessor));
        }

        public ICategoryRepository GetCategoryRepository(IHttpContextAccessor accessor)
        {
            return _categoryRepositories.FirstOrDefault(r => r.RepositoryType == GetRepositoryType(accessor)); ;
        }

        private RepositoryType GetRepositoryType(IHttpContextAccessor accessor)
        {
            string? repositoryTypeStr = accessor.HttpContext.Request.Headers["repositoryType"];
            RepositoryType repositoryType;
            
            if (repositoryTypeStr == null)
            {
                return _defaultRepositoryType;
            }

            if (Enum.TryParse(repositoryTypeStr, out repositoryType))
            {
                return repositoryType;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"The value '{repositoryTypeStr}' is not a valid repository type.");
            }
        }
    }
}
