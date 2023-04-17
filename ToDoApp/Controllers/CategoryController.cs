using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Interfaces;
using ToDoApp.Models;
using ToDoApp.Repository;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepository = categoryRepo;
            _mapper = mapper;   
        }

        [HttpPost]
        public RedirectResult Create(CreateCategoryViewModel createCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _categoryRepository.Add(_mapper.Map<CategoryDto>(createCategoryViewModel));
                }
                catch (Exception ex) { }
            }
            return Redirect("/");
        }

        [HttpPost]
        public RedirectResult Delete(CategoryByIdViewModel deleteCategoryViewModel)
        {
            _categoryRepository.Delete(deleteCategoryViewModel.Id);
            return Redirect("/");
        }

    }
}
