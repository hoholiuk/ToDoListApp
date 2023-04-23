using BusinessLogic.Repositories;

namespace WebApp
{
    public static class CurrentRepository
    {
        public static RepositoryType repositoryType = RepositoryType.SQL;

        public static void ChangeRepositoryType(RepositoryType repository)
        {
            repositoryType = repository;
        }
    }
}
