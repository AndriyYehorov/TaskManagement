using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs
{
    public class UserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required,EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
