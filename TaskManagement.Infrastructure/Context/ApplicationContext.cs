using Microsoft.EntityFrameworkCore;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Infrastructure.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options): base(options) { }
        
        public DbSet<User> Users { get; set; }

        public DbSet<TaskItem> Tasks { get; set; }
    }
}