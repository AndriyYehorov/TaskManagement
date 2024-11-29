using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Application.DTOs
{
    public record TaskFilter(
        [EnumDataType(typeof(Status))] 
        Status? Status,

        DateTime? MinDueDate,

        DateTime? MaxDueDate,

        [EnumDataType(typeof(Priority))] 
        Priority? Priority);
}
