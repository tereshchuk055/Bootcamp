using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ToDoApp.ViewModels
{
    public class CreateCategoryViewModel
    {
        [Required(ErrorMessage = "You must enter category name!")]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
