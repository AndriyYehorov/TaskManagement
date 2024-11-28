using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs
{
    public class TaskDTO
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime DueDate { get; set; }

        public Status Status { get; set; }

        public Priority Priority { get; set; }
    }
}