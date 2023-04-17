using Microsoft.AspNetCore.Mvc;
using ToDoApp.Models;

namespace ToDoApp.Interfaces
{
    public interface ITaskRepository
    {
        List<TaskDto> Get();

        List<TaskDto> GetByCategory(int categoryId);

        void Add(TaskDto task);

        void Delete(int id);

        public void ChangeCompletedState(int id, bool state);
    }
}
