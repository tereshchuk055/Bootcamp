using ToDoApp.Services;

namespace ToDoAppWebAPI.Services
{
    public class RepositoryFactory
    {
        private readonly DbContext _context;

        private readonly StorageType _storageType;

        public RepositoryFactory(DbContext context, StorageType type) 
        {
            _storageType = type;
            _context = context;
        }
        
        public ITaskRepository GetTaskRepository() 
        {
            if (_storageType == StorageType.Sql)

                return new TaskSqlRepository(_context);
            else
                return new TaskXmlRepository(_context);

        }
        
        public ICategoryRepository GetCategoryRepository() 
        {
            if (_storageType == StorageType.Sql)

                return new CategorySqlRepository(_context);
            else
                return new CategoryXmlRepository(_context);

        }
    }
}
