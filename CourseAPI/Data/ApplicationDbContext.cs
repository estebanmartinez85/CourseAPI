using System;
using System.Collections.Generic;
using System.Linq;
using CourseAPI.Data.Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CourseAPI.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CourseAPI.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<CourseUsers>()
                .HasAlternateKey(table => new
                {
                    table.CourseId,
                    table.UserId
                }).HasName("AlternateKey_CourseUsers_CourseId_UserId");
            builder.Entity<Library>()
                 .HasIndex(l => l.Title)
                 .IsUnique();
            builder.Entity<Course>()
                 .HasIndex(c => c.Code)
                 .IsUnique();
            builder.Entity<Course>()
                 .HasIndex(c => c.Title)
                 .IsUnique();
            builder.Entity<Library>()
                .HasMany(l => l.Courses)
                .WithOne(c => c.Library);
            builder.Entity<Storyboard>().Property<string>("GraphicsStr").HasField("_graphics");
            builder.Entity<Storyboard>().Property<string>("NarrationStr").HasField("_narration");
            builder.Entity<Timesheet>().Property<string>("RowsStr").HasField("_rows");
            //builder.Entity<TimesheetRow>().Property<string>("WeekStr").HasField("_week");
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Storyboard> Storyboards { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<CourseUsers> CourseUsers { get; set; }
        public DbSet<Timesheet> Timesheets { get; set; }
        public DbSet<TimesheetTask> TimesheetTasks { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            IEnumerable<EntityEntry> changedEntries = this.ChangeTracker.Entries()
                .Where(
                    e => e.Entity is IAuditInfo && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (EntityEntry entry in changedEntries)
            {
                IAuditInfo entity = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added && entity.CreatedOn == default(DateTime))
                {
                    entity.CreatedOn = DateTime.UtcNow;
                }
                else
                {
                    entity.ModifiedOn = DateTime.UtcNow;
                }
            }
        }
    }
}
