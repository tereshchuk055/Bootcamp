using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    [Table("Task")]
    public class TaskDto : IModel
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime Deadline { get; set; }
        [Required]
        public bool IsCompleted { get; set; }
    }
}