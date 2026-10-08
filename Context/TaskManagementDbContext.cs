using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using TaskManagement.Model;

namespace TaskManagement.Context
{
    public class TaskManagementDbContext : DbContext
    {
        public TaskManagementDbContext(DbContextOptions<TaskManagementDbContext> db) : base(db)
        {
           
        }

        public DbSet<TaskItem> Tasks { get; set; }
        public DbSet<User> Users { get; set; }

        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
