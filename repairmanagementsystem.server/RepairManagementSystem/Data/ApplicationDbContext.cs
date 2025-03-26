using Microsoft.EntityFrameworkCore;
using RepairManagementSystem.Models;

namespace RepairManagementSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<RepairActivity> RepairsActivities { get; set; }
        public DbSet<RepairActivityType> RepairsActivitiesTypes { get; set; }
        public DbSet<RepairObjectType> RepairsObjectsTypes { get; set; }
        public DbSet<RepairRequest> RepairsRequests { get; set; }
        public DbSet<RepairTask> RepairsTasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserToken> UsersTokens { get; set; }
        public DbSet<Worker> Workers { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RepairTask>()
                .HasOne(rt => rt.RepairRequest)
                .WithMany(rr => rr.RepairsTasks)
                .HasForeignKey(rt => rt.RepairRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RepairActivity>()
                .HasOne(ra => ra.Worker)
                .WithMany(w => w.RepairsActivities) 
                .HasForeignKey(ra => ra.WorkerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Add more relationships here if needed in the future
        }
    }
}

// * EXAMPLE SQL QUERY TO FILL OUR DATABASE *
/*

SET IDENTITY_INSERT Users ON;
INSERT INTO Users (UserId, FirstName, LastName, Email, PasswordHash, Number, Address_Country, Address_City, Address_PostalCode, Address_Street, Address_ApartNumber, Role, CreatedAt, LastLoginAt)
VALUES
(1, 'Michal', 'Kamyk', 'mk@test.com','#####','123456789', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', 'Admin', '2025-01-15 10:00:00', '2025-02-14 10:00:00'),
(2, 'Michal', 'Mufinka', 'mf@test.com','!!!!!','123321123', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', 'Worker', '2025-02-15 10:00:00', '2025-03-14 10:00:00'),
(3, 'Michal', 'Pizza', 'mp@test.com','@@@@@','420420420', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', 'Customer', '2025-03-15 10:00:00', '2025-04-14 10:00:00'),
(4, 'Michal', 'Boss', 'mb@test.com','*****','696969696', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', 'Manager', '2025-03-15 10:00:00', '2025-04-14 10:00:00');
SET IDENTITY_INSERT Users OFF;

SET IDENTITY_INSERT UsersTokens ON;
INSERT INTO UsersTokens (UserTokenId, UserId, RefreshToken, CreatedAt, ValidUntil)
VALUES
(1, 1, 'here will be something', '2025-01-15 10:00:00', '2025-06-15 10:00:00'),
(2, 2, 'here will be something', '2025-02-15 10:00:00', '2025-07-15 10:00:00'),
(3, 3, 'here will be something', '2025-03-15 10:00:00', '2025-08-15 10:00:00');
SET IDENTITY_INSERT UsersTokens OFF;

INSERT INTO Customers (UserId, PaymentMethod)
VALUES
(3, 'BLIK');

INSERT INTO Managers (UserId, Expertise, ActiveRepairsCount)
VALUES
(4, 'Cars', '1');

INSERT INTO Workers (UserId, Expertise, IsAvailable)
VALUES
(2, 'DBs', 1);

SET IDENTITY_INSERT RepairsObjectsTypes ON;
INSERT INTO RepairsObjectsTypes (RepairObjectTypeId, Name)
VALUES
(1, 'Car'),
(2, 'PC');
SET IDENTITY_INSERT RepairsObjectsTypes OFF;

SET IDENTITY_INSERT RepairsRequests ON;
INSERT INTO RepairsRequests (RepairRequestId, Name, RepairObjectTypeId, CustomerId, IsPaid, CreatedAt, StartedAt, FinishedAt)
VALUES 
(1, 'My car is making br br br', 1, 3, 1, '2025-03-15 10:00:00', '2025-03-17 10:00:00', '2025-03-20 10:00:00'),
(2, 'My PC doesn"t run', 2, 3, 1, '2025-04-15 10:00:00', '2025-04-17 10:00:00', '2025-04-20 10:00:00');
SET IDENTITY_INSERT RepairsRequests OFF;

SET IDENTITY_INSERT RepairsTasks ON;
INSERT INTO RepairsTasks (RepairTaskId, Description, Result, Status, RepairRequestId, ManagerId, CreatedAt, StartedAt, FinishedAt)
VALUES
(1, 'Looked into the engine', 'Something was wrong.', 'FIN', 1, 4, '2025-03-17 10:00:00', '2025-03-17 12:00:00', '2025-03-17 15:30:00'),
(2, 'Looked into the engine', 'There were not the engine gasket', 'FIN', 1, 4, '2025-03-18 11:00:00', '2025-03-18 12:12:12', '2025-03-18 14:30:00'),
(3, 'Looked into the motherboard', 'There was everything burned', 'FIN', 2, 4, '2025-04-16 10:00:00', '2025-04-16 17:00:00', '2025-04-20 10:00:00');
SET IDENTITY_INSERT RepairsTasks OFF;

SET IDENTITY_INSERT RepairsActivitiesTypes ON;
INSERT INTO RepairsActivitiesTypes (RepairActivityTypeId, Name)
VALUES
(1, 'Motherboard changing'),
(2, 'Adding new oil'),
(3, 'Replacement the gasket');
SET IDENTITY_INSERT RepairsActivitiesTypes OFF;

SET IDENTITY_INSERT RepairsActivities ON;
INSERT INTO RepairsActivities (RepairActivityId, RepairActivityTypeId, SequenceNumber, Description, Result, Status, RepairTaskId, WorkerId, CreatedAt, StartedAt, FinishedAt)
VALUES
(1, 1, 1, 'We have added the new oil.', 'New oil added.', 'FIN', 1, 2, '2025-03-17 10:00:00', '2025-03-17 12:00:00', '2025-03-17 15:30:00'),
(2, 3, 2, 'Changing the gasket.', 'New gasket bought and replaced.', 'FIN', 1, 2, '2025-03-18 11:00:00', '2025-03-18 12:12:12', '2025-03-18 14:30:00'),
(3, 2, 1, 'We have bought a new motherboard.', 'PC now works', 'FIN', 2, 2, '2025-04-16 10:00:00', '2025-04-16 17:00:00', '2025-04-20 10:00:00');
SET IDENTITY_INSERT RepairsActivities OFF;

*/
// * EXAMPLE SQL QUERY TO FILL OUR DATABASE *
