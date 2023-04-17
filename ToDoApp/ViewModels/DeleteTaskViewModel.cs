using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.ViewModels
{
    public class DeleteTaskViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
