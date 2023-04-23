using ToDoApp.Interfaces;
using ToDoApp.Repository;
using ToDoApp.Enums;

namespace ToDoApp.Services
{
    public class TaskRepositoryFactory
    {
        private DapperContext _context;
        private ChosenRepositoryService _repository;

        private readonly TaskSqlRepository _taskSqlRepository;
        private readonly TaskXmlRepository _taskXmlRepository;

        public TaskRepositoryFactory(DapperContext context, ChosenRepositoryService repository) 
        {
            _context = context;
            _repository = repository;
            _taskSqlRepository = new TaskSqlRepository(_context);
            _taskXmlRepository = new TaskXmlRepository(_context);
        }
        
        public ITaskRepository GetRepository() 
        {
            if(_repository.StorageType == StorageType.Sql) 
            {
                return _taskSqlRepository;
            }
            else 
            {
                return _taskXmlRepository;
            }
        }
    }
}
