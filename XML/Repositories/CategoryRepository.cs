using System.Xml.Serialization;
using BusinessLogic.Models;
using BusinessLogic.Repositories;
using Microsoft.Extensions.Configuration;

namespace XML.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        public RepositoryType RepositoryType { get => RepositoryType.XML; }
        private string categoriesFilePath;
        private string tasksFilePath;

        XmlSerializer categoryXmlSerializer;
        XmlSerializer taskXmlSerializer;

        public CategoryRepository(IConfiguration configuration)
        {
            categoriesFilePath = configuration.GetSection("XMLStorages")["CategoryStorage"];
            tasksFilePath = configuration.GetSection("XMLStorages")["TaskStorage"];
            categoryXmlSerializer = new XmlSerializer(typeof(List<CategoryModel>));
            taskXmlSerializer = new XmlSerializer(typeof(List<TaskModel>));
        }

        public IEnumerable<CategoryModel> GetCategoriesList()
        {
            List<CategoryModel> categories;

            using (FileStream fs = new FileStream(categoriesFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    categories = (List<CategoryModel>)categoryXmlSerializer.Deserialize(fs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("[Exception]: " + ex.Message);
                    categories = new List<CategoryModel>();
                }
            }

            return categories;
        }

        public void Create(CategoryModel category)
        {
            List<CategoryModel> categories = (List<CategoryModel>)GetCategoriesList();

            if (categories.Count > 0)
                category.Id = categories.Max(t => t.Id) + 1;

            categories.Add(category);

            using (FileStream fs = new FileStream(categoriesFilePath, FileMode.OpenOrCreate))
            {
                categoryXmlSerializer.Serialize(fs, categories);
            }
        }

        public void Delete(int id)
        {
            List<CategoryModel> categories = (List<CategoryModel>)GetCategoriesList();

            if(categories.RemoveAll(c => c.Id == id) > 0)
            {
                using (FileStream fs = new FileStream(categoriesFilePath, FileMode.Truncate))
                {
                    categoryXmlSerializer.Serialize(fs, categories);
                }

                RemoveCategoryLinks(id);
            }
        }

        private void RemoveCategoryLinks(int id)
        {
            List<TaskModel> tasks;

            using (FileStream fs = new FileStream(tasksFilePath, FileMode.OpenOrCreate))
            {
                try
                {
                    tasks = (List<TaskModel>)taskXmlSerializer.Deserialize(fs);
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

            using (FileStream fs = new FileStream(tasksFilePath, FileMode.Truncate))
            {
                taskXmlSerializer.Serialize(fs, tasks);
            }
        }
    }
}
