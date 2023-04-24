using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.Storage;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepositoryFactory _taskRepository;
        private StorageType _storageType;
        private readonly IMapper _mapper;

        public TaskController(TaskRepositoryFactory taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _storageType = StorageType.Sql;
        }

        [HttpPost]
        public RedirectResult Create(CreateTaskViewModel createTaskViewModel)
        {
            if (!Enum.TryParse(HttpContext.Request.Cookies["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
                HttpContext.Response.Cookies.Append("StorageType", StorageType.Sql.ToString());
            }

            _taskRepository.GetRepository(_storageType).Add(_mapper.Map<TaskDto>(createTaskViewModel));
            return Redirect("/");
        }

        [HttpPost]
        public RedirectResult Delete(DeleteTaskViewModel deleteTaskViewModel)
        {
            if (!Enum.TryParse(HttpContext.Request.Cookies["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
                HttpContext.Response.Cookies.Append("StorageType", StorageType.Sql.ToString());
            }

            _taskRepository.GetRepository(_storageType).Delete(deleteTaskViewModel.Id);
            return Redirect("/");
        }
        
        [HttpPost]
        public RedirectResult ChangeCompleted(ChangeCompletedStateViewModel changeCompletedStateViewModel)
        {
            if (!Enum.TryParse(HttpContext.Request.Cookies["StorageType"], out _storageType))
            {
                _storageType = StorageType.Sql;
                HttpContext.Response.Cookies.Append("StorageType", StorageType.Sql.ToString());
            }

            _taskRepository.GetRepository(_storageType).ChangeCompletedState(changeCompletedStateViewModel.Id, changeCompletedStateViewModel.IsCompleted);
            return Redirect("/");
        }
    }
}
