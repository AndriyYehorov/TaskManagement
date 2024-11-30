using System.Text.Json.Serialization;
using TaskManagement.Application.Enums;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.DTOs
{
    public record ServiceResponse<T>(

        [property: JsonConverter(typeof(JsonStringEnumConverter))]
        ServiceResult Result,

        string? Message = null,

        T? Data = default);
}
