using Microsoft.AspNetCore.Mvc;
using ToDoApp.ViewModels;
using ToDoApp.Interfaces;

namespace ToDoApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;

        public HomeController(ITaskRepository taskRepo, ICategoryRepository categoryRepository)
        {
            _taskRepository = taskRepo;
            _categoryRepository = categoryRepository;

        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel()
            {
                Tasks = _taskRepository.Get(),
                Categories = _categoryRepository.Get()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Sort(CategoryByIdViewModel categoryByIdViewModel)
        {
            var vm = new HomeViewModel()
            {
                Tasks = _taskRepository.GetByCategory(categoryByIdViewModel.Id),
                Categories = _categoryRepository.Get()
            };
            return View("Index", vm);
        }
    }
}