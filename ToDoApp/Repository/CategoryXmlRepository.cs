using Microsoft.Extensions.Options;
using System.Xml.Linq;
using System.Linq;
using ToDoApp.Interfaces;
using ToDoApp.Models;
using ToDoApp.Services;
using System.Threading.Tasks;

namespace ToDoApp.Repository
{
    public class CategoryXmlRepository : ICategoryRepository
    {
        private XDocument _document;
        private readonly string _storagePath;

        public CategoryXmlRepository(DapperContext context)
        {
            _storagePath = context.GetStoragePath();
        }

        public void Add(CategoryDto category)
        {
            _document = XDocument.Load(_storagePath);

            int nextId;
            try
            {
                nextId = (int)((XElement)_document.Root
                      .Element("Categories")
                      .LastNode)
                      .Element("Id") + 1;
            }
            catch (Exception e)
            {
                nextId = 0;
            }

            XElement categoryElement = new XElement("Category",
                new XElement("Id", nextId),
                new XElement("Name", category.Name)
                );

            _document.Root.Element("Categories").Add(categoryElement);
            _document.Save(_storagePath);
        }

        public void Delete(int id)
        {
            _document = XDocument.Load(_storagePath);

            XElement categoryElement = _document.Root
                .Element("Categories")
                .Elements("Category")
                .Where(category => (int)category.Element("Id") == id)
                .FirstOrDefault();

            if (categoryElement != null)
            {
                _document.Root
                    .Element("Tasks")
                    .Elements("Task")
                    .Where(task => (int)task.Element("CategoryId") == id)
                    .Remove();
                categoryElement.Remove();
                _document.Save(_storagePath);
            }
        }

        public List<CategoryDto> Get()
        {
            _document = XDocument.Load(_storagePath);

            return
                _document.Root.Element("Categories")
                    .Elements("Category")
                    .Select(category => new CategoryDto
                    {
                        Id = (int)category.Element("Id"),
                        Name = (string)category.Element("Name")
                    })
                    .ToList() ?? new List<CategoryDto>();
        }
    }
}
