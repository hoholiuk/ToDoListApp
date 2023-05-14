using BusinessLogic.Models;

namespace BusinessLogic.Repositories
{
    public interface ITaskRepository
    {
        RepositoryType RepositoryType { get; }

        IEnumerable<TaskModel> GetAllTasks();

        TaskModel GetById(int id);

        TaskModel Create(TaskModel task);

        TaskModel Update(TaskModel task);

        void Complete(int id);

        void Delete(int id);
    }
}
