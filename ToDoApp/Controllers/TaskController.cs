using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;
using ToDoApp.Services;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskRepositoryFactory _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(TaskRepositoryFactory taskRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public RedirectResult Create(CreateTaskViewModel createTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _taskRepository.GetRepository().Add(_mapper.Map<TaskDto>(createTaskViewModel));
                }
                catch (Exception ex) { }
            }
            return Redirect("/");
        }

        [HttpPost]
        public RedirectResult Delete(DeleteTaskViewModel deleteTaskViewModel)
        {
            _taskRepository.GetRepository().Delete(deleteTaskViewModel.Id);
            return Redirect("/");
        }
        
        [HttpPost]
        public RedirectResult ChangeCompleted(ChangeCompletedStateViewModel changeCompletedStateViewModel)
        {
            _taskRepository.GetRepository().ChangeCompletedState(changeCompletedStateViewModel.Id, changeCompletedStateViewModel.IsCompleted);
            return Redirect("/");
        }
    }
}
