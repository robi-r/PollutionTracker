using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PollutionTracker.Models;

namespace PollutionTracker.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Area> Areas { get; set; }
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<Pollution> Pollutions { get; set; }
        public DbSet<AlertThreshold> AlertThresholds { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primary key for AlertThreshold
            modelBuilder.Entity<AlertThreshold>()
                .HasKey(a => a.ThresholdID);

            // Relationship between Area and Sensor (restrict delete)
            modelBuilder.Entity<Area>()
                .HasMany(a => a.Sensors)
                .WithOne(s => s.Area)
                .HasForeignKey(s => s.AreaID)
                .OnDelete(DeleteBehavior.Restrict);  // Change cascade to restrict

            // Relationship between Sensor and AlertThreshold
            modelBuilder.Entity<Sensor>()
                .HasOne(s => s.AlertThreshold)
                .WithMany()
                .HasForeignKey(s => s.AlertThresholdID)
                .OnDelete(DeleteBehavior.SetNull);  // Set Null for AlertThreshold deletion

            // Relationship between Sensor and Pollution
            modelBuilder.Entity<Sensor>()
                .HasMany(s => s.Pollutions)
                .WithOne(p => p.Sensor)
                .HasForeignKey(p => p.SensorID)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade for Pollution deletion
        }
    }
}
