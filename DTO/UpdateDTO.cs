using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTO
{
    public class UpdateDTO
    {
        [Required]
        public string? TaskName { get; set; }
        [Required]
        public string? Priority { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
    }
}
