using Microsoft.Extensions.Options;
using System.Xml.Linq;
using System.Linq;
using ToDoApp.Models;
using ToDoApp.Services;
using System.Threading.Tasks;

namespace ToDoApp.Repository
{
    public class CategoryXmlRepository : ICategoryRepository
    {
        private XDocument _document;
        private IEnumerable<XElement> _categories;
        private readonly string _storagePath;

        public CategoryXmlRepository(DbContext context)
        {
            _storagePath = context.GetStoragePath();
            _document = new XDocument();
            _categories = Enumerable.Empty<XElement>();
        }

        public void Add(CategoryDto category)
        {
            _document = XDocument.Load(_storagePath);
            _categories = _document.Root?.Element("Categories")?.Elements("Category") ?? Enumerable.Empty<XElement>();
            int nextId = 0;

            if (_categories.Any() && !int.TryParse((_categories.Last<XElement>().FirstNode as XElement).Value, out nextId))
            {
                nextId = 0;
            }
            else
            {
                ++nextId;
            }

            XElement categoryElement = new("Category",
                new XElement("Id", nextId),
                new XElement("Name", category.Name)
            );

            _document.Root?.Element("Categories")?.Add(categoryElement);
            _document.Save(_storagePath);
        }

        public void Delete(int id)
        {
            _document = XDocument.Load(_storagePath);
            _categories = _document.Root?.Element("Categories")?.Elements("Category") ?? Enumerable.Empty<XElement>();

            if (_categories is not null && _categories.Any())
            {
                var categoryElement = 
                    _categories
                    .Where(category => int.TryParse(category?.Element("Id")?.Value, out int value) && value == id)
                    .FirstOrDefault();

                if (categoryElement != null)
                {
                    _document.Root
                    ?.Element("Tasks")
                    ?.Elements("Task")
                    .Where(task => int.TryParse(task?.Element("CategoryId")?.Value, out int value) && value == id)
                    .Remove();

                    categoryElement.Remove();
                    _document.Save(_storagePath);
                }
            }
        }

        public List<CategoryDto> Get()
        {
            _document = XDocument.Load(_storagePath);
            _categories = _document.Root?.Element("Categories")?.Elements("Category") ?? Enumerable.Empty<XElement>();

            if (_categories is not null && _categories.Any())
            {
                return
                    _categories
                    .Select(category => new CategoryDto
                    {
                        Id = (int)category.Element("Id"),
                        Name = (string)category?.Element("Name")
                    })
                     .ToList();
            }
            else
            {
                return new List<CategoryDto>();
            }
        }
    }
}
