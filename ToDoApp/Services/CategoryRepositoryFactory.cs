using ToDoApp.Repository;
using ToDoApp.Storage;

namespace ToDoApp.Services
{
    public class CategoryRepositoryFactory
    {
        private DbContext _context;

        private readonly CategorySqlRepository _categorySqlRepository;
        private readonly CategoryXmlRepository _categoryXmlRepository;

        public CategoryRepositoryFactory(DbContext context) 
        {
            _context = context;
            _categorySqlRepository = new CategorySqlRepository(_context);
            _categoryXmlRepository = new CategoryXmlRepository(_context);
        }

        public ICategoryRepository GetRepository(StorageType storageType)
        {
            if (storageType == StorageType.Sql)
            {
                return _categorySqlRepository;
            }
            else
            {
                return _categoryXmlRepository;
            }
        }
    }
}
