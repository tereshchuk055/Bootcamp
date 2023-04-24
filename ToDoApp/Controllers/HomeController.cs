using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoApp.ViewModels;
using ToDoApp.Services;
using ToDoApp.Storage;

namespace ToDoApp.Controllers
{
   
    public class HomeController : Controller
    {
        private readonly CategoryRepositoryFactory _categoryRepository;
        private readonly TaskRepositoryFactory _taskRepository;
        private StorageType _storageType;

        public HomeController(TaskRepositoryFactory taskRepository, CategoryRepositoryFactory categoryRepository)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _storageType = StorageType.Sql;
        }

        public IActionResult Index()
        {
            if(!Enum.TryParse(HttpContext.Request.Cookies["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
                HttpContext.Response.Cookies.Append("StorageType", StorageType.Sql.ToString());
            }

            List<SelectListItem> storageTypes = new List<SelectListItem>();
            foreach (var type in Enum.GetNames(typeof(StorageType))) 
            {
                storageTypes.Add( new SelectListItem
                {
                    Value = type.ToString(),
                    Text = type.ToString().ToUpper(),
                    Selected = type == _storageType.ToString()
                });
            }

            var vm = new HomeViewModel()
            {
                Tasks = _taskRepository.GetRepository(_storageType).Get(),
                Categories = _categoryRepository.GetRepository(_storageType).Get(),
                Storages = storageTypes
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Sort(CategoryByIdViewModel categoryByIdViewModel)
        {
            if (!Enum.TryParse(HttpContext.Request.Cookies["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
                HttpContext.Response.Cookies.Append("StorageType", StorageType.Sql.ToString());
            }

            List<SelectListItem> storageTypes = new List<SelectListItem>();
            foreach (var type in Enum.GetNames(typeof(StorageType)))
            {
                storageTypes.Add(new SelectListItem
                {
                    Value = type.ToString(),
                    Text = type.ToString().ToUpper(),
                    Selected = type == _storageType.ToString()
                });
            }

            var vm = new HomeViewModel()
            {
                Tasks = _taskRepository.GetRepository(_storageType).GetByCategory(categoryByIdViewModel.Id),
                Categories = _categoryRepository.GetRepository(_storageType).Get(),
                Storages = storageTypes
            };
            return View("Index", vm);
        }

        [HttpPost]
        public RedirectResult ChangeRepository(ChangeRepositoryViewModel changeRepositoryViewModel) 
        {
            HttpContext.Response.Cookies.Append("StorageType", changeRepositoryViewModel.StorageType.ToString());
            
            return Redirect("/");
        }
    }
}