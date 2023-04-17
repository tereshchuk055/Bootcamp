using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.ViewModels
{
    public class CategoryByIdViewModel
    {
        [Required]
        public int Id { get; set; }
    }
}
