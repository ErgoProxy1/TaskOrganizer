using Microsoft.EntityFrameworkCore;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.API.Data
{
    public class TaskOrganizerDbContext : DbContext
    {
        public TaskOrganizerDbContext(DbContextOptions<TaskOrganizerDbContext> options) : base(options) { }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }

    }
}
