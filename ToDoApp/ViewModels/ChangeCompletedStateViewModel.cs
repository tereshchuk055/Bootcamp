using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.ViewModels
{
    public class ChangeCompletedStateViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsCompleted { get; set; }
    }
}
