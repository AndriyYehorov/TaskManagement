using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Application.DTOs
{
    public class LoginDTO
    {
        [Required]
        public string UsernameOrEmail { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
