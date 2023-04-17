using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.ViewModels
{
    public class CreateTaskViewModel
    {
        [Required(ErrorMessage = "Task must have a name!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "You should choose date and time for deadline!")]
        public DateTime Deadline { get; set; }

        [Required(ErrorMessage = "You should choose a category for task!")]
        public int CategoryId { get; set; }
    }
}
