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
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        [RegularExpression(@"^.*([\W_]).*$", ErrorMessage = "Password must contain at least one special character.")]
        public string Password { get; set; }
    }
}
