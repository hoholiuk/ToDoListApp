using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace XML.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public RepositoryType RepositoryType { get => RepositoryType.XML; }
        private XmlSerializer _xmlSerializer;
        private string _tasksFilePath;

        public TaskRepository(IConfiguration configuration)
        {
            _tasksFilePath = configuration.GetSection("XMLStorages")["TaskStorage"];
            _xmlSerializer = new XmlSerializer(typeof(List<TaskModel>));
        }

        public IEnumerable<TaskModel> GetAllTasks()
        {
            List<TaskModel> tasks;

            using (FileStream fs = new FileStream(_tasksFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    tasks = (List<TaskModel>)_xmlSerializer.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception]: " + ex.Message);
                    tasks = new List<TaskModel>();
                }
            }

            return GetSortedTasksList(tasks);
        }

        public TaskModel GetById(int id)
        {
            List<TaskModel> tasks = GetAllTasks().ToList();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public void Complete(int id)
        {
            List<TaskModel> tasks = GetAllTasks().ToList();

            var task = tasks.FirstOrDefault(t => t.Id == id);

            task.IsCompleted = !task.IsCompleted;

            using (FileStream fs = new FileStream(_tasksFilePath, FileMode.Truncate))
            {
                _xmlSerializer.Serialize(fs, tasks);
            }
        }

        public TaskModel Create(TaskModel task)
        {
            List<TaskModel> tasks = GetAllTasks().ToList();

            if (tasks.Count > 0)
                task.Id = tasks.Max(t => t.Id) + 1;

            tasks.Add(task);

            using (FileStream fs = new FileStream(_tasksFilePath, FileMode.OpenOrCreate))
            {
                _xmlSerializer.Serialize(fs, tasks);
            }

            return task;
        }

        public void Delete(int id)
        {
            List<TaskModel> tasks = GetAllTasks().ToList();

            tasks.RemoveAll(c => c.Id == id);

            using (FileStream fs = new FileStream(_tasksFilePath, FileMode.Truncate))
            {
                _xmlSerializer.Serialize(fs, tasks);
            }
        }

        public TaskModel Update(TaskModel updatedTask)
        {
            List<TaskModel> tasks = GetAllTasks().ToList();

            var task = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);

            task.Title = updatedTask.Title;
            task.IsCompleted = updatedTask.IsCompleted;
            task.DueDate = updatedTask.DueDate;
            task.CategoryId = updatedTask.CategoryId;

            using (FileStream fs = new FileStream(_tasksFilePath, FileMode.Truncate))
            {
                _xmlSerializer.Serialize(fs, tasks);
            }

            return updatedTask;
        }

        private IEnumerable<TaskModel> GetSortedTasksList(IEnumerable<TaskModel> tasks)
        {
            List<TaskModel> tasksWithDueDate = new List<TaskModel>();
            List<TaskModel> tasksWithoutDueDate = new List<TaskModel>();

            foreach (var task in tasks)
            {
                if (task.DueDate == null) tasksWithoutDueDate.Add(task);
                else tasksWithDueDate.Add(task);
            }

            tasksWithDueDate = tasksWithDueDate.OrderBy(x => x.DueDate).ToList();

            return tasksWithDueDate.Concat(tasksWithoutDueDate).ToList();
        }
    }
}
