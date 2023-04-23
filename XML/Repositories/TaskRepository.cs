using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;
using System.Xml.Serialization;

namespace XML.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        public RepositoryType RepositoryType { get => RepositoryType.XML; }
        private string tasksFilePath;

        private XmlSerializer xmlSerializer;

        public TaskRepository(IConfiguration configuration)
        {
            this.tasksFilePath = configuration.GetSection("XMLStorages")["TaskStorage"];
            this.xmlSerializer = new XmlSerializer(typeof(List<TaskModel>));
        }

        public TaskModel GetById(int id)
        {
            List<TaskModel> tasks = (List<TaskModel>)GetTasksList();
            return tasks.FirstOrDefault(t => t.Id == id);
        }

        public IEnumerable<TaskModel> GetTasksList()
        {
            List<TaskModel> tasks;

            using (FileStream fs = new FileStream(tasksFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    tasks = (List<TaskModel>)xmlSerializer.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception]: " + ex.Message);
                    tasks = new List<TaskModel>();
                }
            }

            return GetSortedTasksList(tasks);
        }

        public void Complete(int id)
        {
            List<TaskModel> tasks = (List<TaskModel>)GetTasksList();

            var task = tasks.FirstOrDefault(t => t.Id == id);

            task.IsCompleted = !task.IsCompleted;

            using (FileStream fs = new FileStream(tasksFilePath, FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, tasks);
            }
        }

        public void Create(TaskModel task)
        {
            List<TaskModel> tasks = (List<TaskModel>)GetTasksList();

            if (tasks.Count > 0)
                task.Id = tasks.Max(t => t.Id) + 1;

            tasks.Add(task);

            using (FileStream fs = new FileStream(tasksFilePath, FileMode.OpenOrCreate))
            {
                xmlSerializer.Serialize(fs, tasks);
            }
        }

        public void Delete(int id)
        {
            List<TaskModel> tasks = (List<TaskModel>)GetTasksList();

            tasks.RemoveAll(c => c.Id == id);

            using (FileStream fs = new FileStream(tasksFilePath, FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, tasks);
            }
        }

        public void Update(TaskModel updatedTask)
        {
            List<TaskModel> tasks = (List<TaskModel>)GetTasksList();

            var task = tasks.FirstOrDefault(t => t.Id == updatedTask.Id);

            task.Title = updatedTask.Title;
            task.IsCompleted = updatedTask.IsCompleted;
            task.DueDate = updatedTask.DueDate;
            task.CategoryId = updatedTask.CategoryId;

            using (FileStream fs = new FileStream(tasksFilePath, FileMode.Truncate))
            {
                xmlSerializer.Serialize(fs, tasks);
            }
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
