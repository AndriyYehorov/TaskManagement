using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs
{
    public class TaskDTO
    {
        [Required]
        public string Title { get; set; }

        public string? Description { get; set; }

        public DateTime? DueDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Status))]
        public Status Status { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        [EnumDataType(typeof(Priority))]
        public Priority Priority { get; set; }
    }
}