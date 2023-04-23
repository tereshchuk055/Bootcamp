using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepositoryFactory _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(CategoryRepositoryFactory repositoryFactory, IMapper mapper)
        {
            _categoryRepository = repositoryFactory;
            _mapper = mapper;   
        }

        [HttpPost]
        public RedirectResult Create(CreateCategoryViewModel createCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.GetRepository().Add(_mapper.Map<CategoryDto>(createCategoryViewModel));
                }
                catch (Exception ex) { }
            }
            return Redirect("/");
        }

        [HttpPost]
        public RedirectResult Delete(CategoryByIdViewModel deleteCategoryViewModel)
        {
            _categoryRepository.GetRepository().Delete(deleteCategoryViewModel.Id);
            return Redirect("/");
        }

    }
}
