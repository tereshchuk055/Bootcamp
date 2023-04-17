using ToDoApp.Models;

namespace ToDoApp.ViewModels
{
    public class HomeViewModel
    {
        public List<CategoryDto> Categories { get; set; }
        public List<TaskDto> Tasks { get; set; }

        public CreateTaskViewModel CreateTaskViewModel { get; set; }
        public ChangeCompletedStateViewModel ChangeCompletedStateViewModel { get; set; }
    }
}
