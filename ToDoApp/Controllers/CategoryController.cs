using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Storage;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepositoryFactory _categoryRepository;
        private StorageType _storageType;
        private readonly IMapper _mapper;

        public CategoryController(CategoryRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _categoryRepository = repositoryFactory;
            _mapper = mapper;
            _storageType = StorageType.Sql;
        }

        [HttpPost]
        public RedirectResult Create(CreateCategoryViewModel createCategoryViewModel)
        {
            if (!Enum.TryParse(HttpContext.Request.Cookies["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
                HttpContext.Response.Cookies.Append("StorageType", StorageType.Sql.ToString());
            }

            _categoryRepository.GetRepository(_storageType).Add(_mapper.Map<CategoryDto>(createCategoryViewModel));
             return Redirect("/");
        }

        [HttpPost]
        public RedirectResult Delete(CategoryByIdViewModel deleteCategoryViewModel)
        {
            if (!Enum.TryParse(HttpContext.Request.Cookies["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
                HttpContext.Response.Cookies.Append("StorageType", StorageType.Sql.ToString());
            }

            _categoryRepository.GetRepository(_storageType).Delete(deleteCategoryViewModel.Id);
            return Redirect("/");
        }

    }
}
