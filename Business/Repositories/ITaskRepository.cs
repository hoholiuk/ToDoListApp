using BusinessLogic.Models;

namespace BusinessLogic.Repositories
{
    public interface ITaskRepository
    {
        IEnumerable<TaskModel> GetTasksList();
        TaskModel GetById(int id);
        int Create(TaskModel task);
        int Complete(int id);
        int Delete(int id);
    }
}
