using ToDoApp.Services;

namespace ToDoAppWebAPI.Services
{
    public class RepositoryFactory
    {
        private DbContext _context;

        private StorageType _storageType;
        HttpContextAccessor _httpContextAccessor;

        public RepositoryFactory(DbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = (HttpContextAccessor)httpContextAccessor;
        }
        
        public ITaskRepository GetTaskRepository() 
        {
            if (!Enum.TryParse(_httpContextAccessor?.HttpContext?.Request.Headers["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
            }

            if (_storageType == StorageType.Sql)

                return new TaskSqlRepository(_context);
            else
                return new TaskXmlRepository(_context);

        }
        
        public ICategoryRepository GetCategoryRepository() 
        {
            if (!Enum.TryParse(_httpContextAccessor?.HttpContext?.Request.Headers["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
            }

            if (_storageType == StorageType.Sql)

                return new CategorySqlRepository(_context);
            else
                return new CategoryXmlRepository(_context);

        }
    }
}
