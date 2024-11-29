using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class TaskItem
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Status Status { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Priority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }        

        public Guid UserId { get; set; }
        
        public User? User { get; set; }
    }
}
