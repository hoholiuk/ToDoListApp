using BusinessLogic.Models;

namespace BusinessLogic.Repositories
{
    public interface ITaskRepository
    {
        RepositoryType RepositoryType { get; }

        IEnumerable<TaskModel> GetTasksList();

        TaskModel GetById(int id);

        void Create(TaskModel task);

        void Update(TaskModel task);

        void Complete(int id);

        void Delete(int id);
    }
}
