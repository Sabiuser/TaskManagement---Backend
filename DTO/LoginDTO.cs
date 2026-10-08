using System.ComponentModel.DataAnnotations;

namespace TaskManagement.DTO
{
    public class LoginDTO
    {
        [EmailAddress]
        [Required]
        public string UserEmail { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
