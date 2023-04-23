using System.Collections.Generic;
using System.Xml.Linq;
using ToDoApp.Interfaces;
using ToDoApp.Models;
using ToDoApp.Services;

namespace ToDoApp.Repository
{
    public class TaskXmlRepository : ITaskRepository
    {
        private XDocument _document;
        private readonly string _storagePath;

        public TaskXmlRepository(DapperContext context)
        {
            _storagePath = context.GetStoragePath();
        }

        public void Add(TaskDto task)
        {
            _document = XDocument.Load(_storagePath);

            int nextId;
            try 
            {
                nextId = (int)((XElement)_document.Root
                      .Element("Tasks")
                      .LastNode)
                      .Element("Id") + 1;
            }
            catch(Exception e) 
            {
                nextId = 0;
            }
            
            XElement taskElement = new XElement("Task",
                new XElement("Id", nextId),
                new XElement("Name", task.Name),
                new XElement("CategoryId", task.CategoryId),
                new XElement("Deadline", task.Deadline),
                new XElement("IsCompleted", false)
                ); 
            
            _document.Root.Element("Tasks").Add(taskElement);
            _document.Save(_storagePath);
        }

        public void ChangeCompletedState(int id, bool state)
        {
            _document = XDocument.Load(_storagePath);

            XElement taskElement = _document.Root
                .Element("Tasks")
                .Elements("Task")
                .Where(task => (int)task.Element("Id") == id)
                .FirstOrDefault();

            if (taskElement != null)
            {
                taskElement.Element("IsCompleted").Value = state.ToString();
                _document.Save(_storagePath);
            }
        }

        public void Delete(int id)
        {
            _document = XDocument.Load(_storagePath);

            XElement taskElement = _document.Root
                .Element("Tasks")
                .Elements("Task")
                .Where(task => (int)task.Element("Id") == id)
                .FirstOrDefault();

            if (taskElement != null)
            {
                taskElement.Remove();
                _document.Save(_storagePath);
            }
        }

        public List<TaskDto> Get()
        {
            _document = XDocument.Load(_storagePath);
            List<TaskDto> tasks = new List<TaskDto>();

            tasks.AddRange(_document.Root.Element("Tasks")
                    .Elements("Task")
                    .Where(task => (bool)task.Element("IsCompleted") == false)
                    .Select(task => new TaskDto 
                    {
                        Id = (int)task.Element("Id"),
                        Name = (string)task.Element("Name"),
                        Deadline = (DateTime)task.Element("Deadline"),
                        IsCompleted = (bool)task.Element("IsCompleted")
                    }).ToList());
            tasks.AddRange(_document.Root.Element("Tasks")
                    .Elements("Task")
                    .Where(task => (bool)task.Element("IsCompleted") == true)
                    .Select(task => new TaskDto
                    {
                        Id = (int)task.Element("Id"),
                        Name = (string)task.Element("Name"),
                        Deadline = (DateTime)task.Element("Deadline"),
                        IsCompleted = (bool)task.Element("IsCompleted")
                    }).ToList());

            return tasks;

        }

        public List<TaskDto> GetByCategory(int categoryId)
        {
            _document = XDocument.Load(_storagePath);
            List<TaskDto> tasks = new List<TaskDto>();

            tasks.AddRange(_document.Root
               .Element("Tasks")
               .Elements("Task")
               .Where(task => (int)task.Element("CategoryId") == categoryId)
               .Where(task => (bool)task.Element("IsCompleted") == false)
               .Select(task => new TaskDto
               {
                   Id = (int)task.Element("Id"),
                   Name = (string)task.Element("Name"),
                   Deadline = (DateTime)task.Element("Deadline"),
                   IsCompleted = (bool)task.Element("IsCompleted")
               })
               .ToList());

            tasks.AddRange(_document.Root
               .Element("Tasks")
               .Elements("Task")
               .Where(task => (int)task.Element("CategoryId") == categoryId)
               .Where(task => (bool)task.Element("IsCompleted") == true)
               .Select(task => new TaskDto
               {
                   Id = (int)task.Element("Id"),
                   Name = (string)task.Element("Name"),
                   Deadline = (DateTime)task.Element("Deadline"),
                   IsCompleted = (bool)task.Element("IsCompleted")
               })
               .ToList());

            return tasks;
        }
    }
}
