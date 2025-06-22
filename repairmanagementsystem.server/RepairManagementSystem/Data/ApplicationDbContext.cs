using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Models;

namespace RepairManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<RepairActivity> RepairActivities { get; set; }
        public DbSet<RepairActivityType> RepairActivityTypes { get; set; }
        public DbSet<RepairObjectType> RepairObjectTypes { get; set; }
        public DbSet<RepairObject> RepairObjects { get; set; }
        public DbSet<RepairRequest> RepairRequests { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<Worker> Workers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepairRequest>()
                .HasOne(rq => rq.RepairObject)
                .WithMany()
                .HasForeignKey(rq => rq.RepairObjectId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RepairActivity>()
                .HasOne(ra => ra.Worker)
                .WithMany(w => w.RepairActivities) 
                .HasForeignKey(ra => ra.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Add more relationships here if needed in the future
        }
    }
}
