using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Interfaces;

namespace ToDoApp.Models
{
    [Table("Category")]
    public class CategoryDto : IModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        
    }
}