using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Repository
{
    public class TaskXmlRepository : ITaskRepository
    {
        private XDocument _document;
        private IEnumerable<XElement> _tasks;
        private readonly string _storagePath;

        public TaskXmlRepository(DbContext context)
        {
            _storagePath = context.GetStoragePath();
            _document = new XDocument();
            _tasks = Enumerable.Empty<XElement>();
        }

        public void Add(TaskDto task)
        {
            _document = XDocument.Load(_storagePath);
            _tasks = _document.Root?.Element("Tasks")?.Elements("Task") ?? Enumerable.Empty<XElement>();
            int nextId = 0;
            if (_tasks.Any() && !int.TryParse((_tasks.Last<XElement>().FirstNode as XElement).Value, out nextId))
            {
                nextId = 0;
            }
            else
            {
                ++nextId;
            }

            XElement taskElement = new("Task",
            new XElement("Id", nextId),
            new XElement("Name", task.Name),
            new XElement("CategoryId", task.CategoryId),
            new XElement("Deadline", task.Deadline),
            new XElement("IsCompleted", false)
            );

            _document.Root?.Element("Tasks")?.Add(taskElement);
            _document.Save(_storagePath);
        }

        public void ChangeCompletedState(int id, bool state)
        {
            _document = XDocument.Load(_storagePath);

            var taskElement = 
                _document.Root
                ?.Element("Tasks")
                ?.Elements("Task")
                .Where(task => (int)task.Element("Id") == id)
                .FirstOrDefault() ?? null;

            if (taskElement != null)
            {
                taskElement.Element("IsCompleted").Value = state.ToString();
                _document.Save(_storagePath);
            }
        }

        public void Delete(int id)
        {
            _document = XDocument.Load(_storagePath);
            _tasks = _document.Root?.Element("Tasks")?.Elements("Task") ?? Enumerable.Empty<XElement>();

            if (_tasks is not null && _tasks.Any())
            {
                _tasks
                .Where(task => int.TryParse(task?.Element("Id")?.Value, out int value) && value == id)
                .Remove();

                _document.Save(_storagePath);
            }
        }

        public List<TaskDto> Get()
        {
            _document = XDocument.Load(_storagePath);
            _tasks = _document.Root?.Element("Tasks")?.Elements("Task") ?? Enumerable.Empty<XElement>();

            if (_tasks is not null && _tasks.Any())
            {
                return 
                    _tasks
                    .Select(task => new TaskDto
                    {
                        Id = (int)task.Element("Id"),
                        Name = (string)task.Element("Name"),
                        Deadline = (DateTime)task.Element("Deadline"),
                        IsCompleted = (bool)task.Element("IsCompleted"),
                        CategoryId = (int)task.Element("CategoryId")
                    })
                    .OrderBy(o => o.IsCompleted)
                    .ToList();
            }
            else
            {
                return new List<TaskDto>();
            }
        }

        public List<TaskDto> GetByCategory(int categoryId)
        {
            _document = XDocument.Load(_storagePath);
            _tasks = _document.Root?.Element("Tasks")?.Elements("Task") ?? Enumerable.Empty<XElement>();

            if (_tasks is not null && _tasks.Any())
            {
                return 
                _tasks
               .Where(task => (int)task.Element("CategoryId") == categoryId)
               .Select(task => new TaskDto
               {
                   Id = (int)task.Element("Id"),
                   Name = (string)task.Element("Name"),
                   Deadline = (DateTime)task.Element("Deadline"),
                   IsCompleted = (bool)task.Element("IsCompleted")
               })
               .OrderBy(o => o.IsCompleted)
               .ToList();
            }
            else
            {
                return new List<TaskDto>();
            }
        }
    }
}
