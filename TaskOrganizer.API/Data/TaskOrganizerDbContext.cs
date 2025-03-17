using Microsoft.EntityFrameworkCore;
using TaskOrganizer.API.Models;

namespace TaskOrganizer.API.Data
{
    public class TaskOrganizerDbContext : DbContext
    {
        public TaskOrganizerDbContext(DbContextOptions<TaskOrganizerDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User *<->* Project //
            modelBuilder.Entity<UserProject>()
                .HasKey(userProject => new { userProject.UserId, userProject.ProjectId });

            modelBuilder.Entity<UserProject>()
                .HasOne(userProject => userProject.User)
                .WithMany(user => user.UserProjects)
                .HasForeignKey(userProject => userProject.UserId);

            modelBuilder.Entity<UserProject>()
                .HasOne(userProject => userProject.Project)
                .WithMany(project => project.UserProjects)
                .HasForeignKey(userProject => userProject.ProjectId);
            // -- End -- //
        }

        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

}
}
