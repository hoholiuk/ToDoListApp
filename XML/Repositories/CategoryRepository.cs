using System.Xml.Serialization;
using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;

namespace XML.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public RepositoryType RepositoryType { get => RepositoryType.XML; }
        private string _categoriesFilePath;
        private string _tasksFilePath;
        private XmlSerializer _categoryXmlSerializer;
        private XmlSerializer _taskXmlSerializer;

        public CategoryRepository(IConfiguration configuration)
        {
            _categoriesFilePath = configuration.GetSection("XMLStorages")["CategoryStorage"];
            _tasksFilePath = configuration.GetSection("XMLStorages")["TaskStorage"];
            _categoryXmlSerializer = new XmlSerializer(typeof(List<CategoryModel>));
            _taskXmlSerializer = new XmlSerializer(typeof(List<TaskModel>));
        }

        public IEnumerable<CategoryModel> GetAllCategories()
        {
            List<CategoryModel> categories;

            using (FileStream fs = new FileStream(_categoriesFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    categories = (List<CategoryModel>)_categoryXmlSerializer.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception]: " + ex.Message);
                    categories = new List<CategoryModel>();
                }
            }

            return categories;
        }

        public CategoryModel GetById(int id)
        {
            List<CategoryModel> categories = GetAllCategories().ToList();
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public CategoryModel Create(CategoryModel category)
        {
            List<CategoryModel> categories = GetAllCategories().ToList();

            if (categories.Count > 0)
                category.Id = categories.Max(t => t.Id) + 1;

            categories.Add(category);

            using (FileStream fs = new FileStream(_categoriesFilePath, FileMode.OpenOrCreate))
            {
                _categoryXmlSerializer.Serialize(fs, categories);
            }

            return category;
        }

        public void Delete(int id)
        {
            List<CategoryModel> categories = GetAllCategories().ToList();

            if(categories.RemoveAll(c => c.Id == id) > 0)
            {
                using (FileStream fs = new FileStream(_categoriesFilePath, FileMode.Truncate))
                {
                    _categoryXmlSerializer.Serialize(fs, categories);
                }

                RemoveCategoryLinks(id);
            }
        }

        private void RemoveCategoryLinks(int id)
        {
            List<TaskModel> tasks;

            using (FileStream fs = new FileStream(_tasksFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    tasks = (List<TaskModel>)_taskXmlSerializer.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception]: " + ex.Message);
                    tasks = new List<TaskModel>();
                }
            }

            foreach (var item in tasks)
            {
                if(item.CategoryId == id)
                    item.CategoryId = null;
            }

            using (FileStream fs = new FileStream(_tasksFilePath, FileMode.Truncate))
            {
                _taskXmlSerializer.Serialize(fs, tasks);
            }
        }
    }
}
