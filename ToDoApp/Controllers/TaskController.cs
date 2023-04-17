using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Interfaces;
using ToDoApp.Models;
using ToDoApp.ViewModels;

namespace ToDoApp.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;

        public TaskController(ITaskRepository taskRepo, IMapper mapper)
        {
            _taskRepository = taskRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public RedirectResult Create(CreateTaskViewModel createTaskViewModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _taskRepository.Add(_mapper.Map<TaskDto>(createTaskViewModel));
                }
                catch (Exception ex) { }
            }
            return Redirect("/");
        }

        [HttpPost]
        public RedirectResult Delete(DeleteTaskViewModel deleteTaskViewModel)
        {
            _taskRepository.Delete(deleteTaskViewModel.Id);
            return Redirect("/");
        }
        
        [HttpPost]
        public RedirectResult ChangeCompleted(ChangeCompletedStateViewModel changeCompletedStateViewModel)
        {
            _taskRepository.ChangeCompletedState(changeCompletedStateViewModel.Id, changeCompletedStateViewModel.IsCompleted);
            return Redirect("/");
        }
    }
}
