using BusinessLogic.Repositories;

namespace WebApp.Services
{
    public class RepositorySwitcher
    {
        private readonly RepositoryType _defaultRepositoryType = RepositoryType.SQL;
        private readonly IEnumerable<ITaskRepository>? _taskRepositories;
        private readonly IEnumerable<ICategoryRepository> _categoryRepositories;

        public RepositorySwitcher(IEnumerable<ITaskRepository> taskRepositories, IEnumerable<ICategoryRepository> categoryRepositories)
        {
            _taskRepositories = taskRepositories;
            _categoryRepositories = categoryRepositories;
        }

        public RepositorySwitcher(IEnumerable<ICategoryRepository> categoryRepositories)
        {
            _categoryRepositories = categoryRepositories;
        }

        public ITaskRepository GetTaskRepository(HttpRequest request)
        {
            return _taskRepositories.FirstOrDefault(r => r.RepositoryType == GetRepositoryType(request));
        }

        public ICategoryRepository GetCategoryRepository(HttpRequest request)
        {
            return _categoryRepositories.FirstOrDefault(r => r.RepositoryType == GetRepositoryType(request));
        }

        private RepositoryType GetRepositoryType(HttpRequest request)
        {
            string? repositoryTypeStr = request.Cookies["repositoryType"];
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
