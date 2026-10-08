using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Model
{
    public class TaskItem
    {
        [Key]
       public int Id { get; set; }
        [Required]
        public string? TaskName { get; set; }
        [Required]
        public string? Priority { get; set; }
        [Required]
        public DateTime DueDate {  get; set; }
    }
}
