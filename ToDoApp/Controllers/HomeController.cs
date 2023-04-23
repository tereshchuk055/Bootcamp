using Microsoft.AspNetCore.Mvc;
using ToDoApp.ViewModels;
using ToDoApp.Services;

namespace ToDoApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly CategoryRepositoryFactory _categoryRepository;
        private readonly TaskRepositoryFactory _taskRepository;
        private readonly ChosenRepositoryService _storageType;

        public HomeController(TaskRepositoryFactory taskRepository, CategoryRepositoryFactory categoryRepository, ChosenRepositoryService repositoryService)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _storageType = repositoryService;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel()
            {
                Tasks = _taskRepository.GetRepository().Get(),
                Categories = _categoryRepository.GetRepository().Get()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Sort(CategoryByIdViewModel categoryByIdViewModel)
        {
            var vm = new HomeViewModel()
            {
                Tasks = _taskRepository.GetRepository().GetByCategory(categoryByIdViewModel.Id),
                Categories = _categoryRepository.GetRepository().Get()
            };
            return View("Index", vm);
        }

        [HttpPost]
        public RedirectResult ChangeRepository(ChangeRepositoryViewModel changeRepositoryViewModel) 
        {
            _storageType.SetStorageType(changeRepositoryViewModel.StorageType);
            return Redirect("/");
        }
    }
}