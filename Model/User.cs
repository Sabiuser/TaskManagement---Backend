using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Model
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Email is required")]
        [EmailAddress]
        public string UserEmail { get; set; } = string.Empty;

        public string PasswordHash { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

    }
}
