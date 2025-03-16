using Microsoft.EntityFrameworkCore;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.API.Data
{
    public class TaskOrganizerDbContext : DbContext
    {
        public TaskOrganizerDbContext(DbContextOptions<TaskOrganizerDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }
        public DbSet<ProjectModel> Projects { get; set; }
        public DbSet<TaskModel> Comments { get; set; }

    }
}
