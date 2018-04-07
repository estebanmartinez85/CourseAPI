using System;
using System.Linq;
using CourseAPI.Data.Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CourseAPI.Models;

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
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            
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
            builder.Entity<SlideCollection>().Property<string>("SlidesStr").HasField("_slides");
            builder.Entity<Course>().Ignore("Links");
            builder.Entity<CourseUsers>().Ignore("Links");
            builder.Entity<Library>().Ignore("Links");
            builder.Entity<Storyboard>().Ignore("Links");
            builder.Entity<Storyboard>().HasOne<SlideCollection>(s => s.Slides);
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Storyboard> Storyboards { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<CourseUsers> CourseUsers { get; set; }

        public override int SaveChanges()
        {
            this.ApplyAuditInfoRules();
            return base.SaveChanges();
        }

        private void ApplyAuditInfoRules()
        {
            var changedEntries = this.ChangeTracker.Entries()
                .Where(
                    e => e.Entity is IAuditInfo && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entry in changedEntries)
            {
                var entity = (IAuditInfo)entry.Entity;
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
