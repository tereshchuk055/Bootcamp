using ToDoApp.Interfaces;
using ToDoApp.Repository;
using ToDoApp.Enums;

namespace ToDoApp.Services
{
    public class CategoryRepositoryFactory
    {
        private DapperContext _context;
        private ChosenRepositoryService _repository;

        private readonly CategorySqlRepository _categorySqlRepository;
        private readonly CategoryXmlRepository _categoryXmlRepository;

        public CategoryRepositoryFactory(DapperContext context, ChosenRepositoryService repository) 
        {
            _context = context;
            _repository = repository;
            _categorySqlRepository = new CategorySqlRepository(_context);
            _categoryXmlRepository = new CategoryXmlRepository(_context);
        }

        public ICategoryRepository GetRepository()
        {
            if (_repository.StorageType == StorageType.Sql)
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
