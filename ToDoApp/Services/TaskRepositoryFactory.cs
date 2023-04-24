using ToDoApp.Repository;
using ToDoApp.Storage;

namespace ToDoApp.Services
{
    public class TaskRepositoryFactory
    {
        private DbContext _context;

        private readonly TaskSqlRepository _taskSqlRepository;
        private readonly TaskXmlRepository _taskXmlRepository;

        public TaskRepositoryFactory(DbContext context) 
        {
            _context = context;
            _taskSqlRepository = new TaskSqlRepository(_context);
            _taskXmlRepository = new TaskXmlRepository(_context);
        }
        
        public ITaskRepository GetRepository(StorageType storageType) 
        {
            if(storageType == StorageType.Sql) 
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
