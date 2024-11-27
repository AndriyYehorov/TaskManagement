﻿using System.ComponentModel.DataAnnotations;
using TaskManagement.Domain.Enums;

namespace TaskManagement.Domain.Entities
{
    public class Task
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime DueDate { get; set; }

        public Status Status { get; set; }

        public Priority Priority { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
