using System.ComponentModel.DataAnnotations;
using ToDoApp.Enums;

namespace ToDoApp.ViewModels
{
    public class ChangeRepositoryViewModel
    {
        [Required]
        public StorageType StorageType { get; set; }
    }
}
