using System.ComponentModel.DataAnnotations;
using ToDoApp.Storage;

namespace ToDoApp.ViewModels
{
    public class ChangeRepositoryViewModel
    {
        [Required]
        public StorageType StorageType { get; set; }
    }
}
