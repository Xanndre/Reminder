using Microsoft.EntityFrameworkCore;
using Reminder.Core.Entities;

namespace Reminder.Infrastructure.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Notification>()
                .HasOne(n => n.Todo)
                .WithMany(t => t.Notifications)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
