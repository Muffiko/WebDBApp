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
                .WithMany(ro => ro.RepairRequests)
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

// * EXAMPLE SQL QUERY TO FILL OUR DATABASE *
/*

SET IDENTITY_INSERT Users ON;
INSERT INTO Users (UserId, FirstName, LastName, Email, PasswordHash, Number, Address_Country, Address_City, Address_PostalCode, Address_Street, Address_ApartNumber, Address_HouseNumber, Role, CreatedAt, LastLoginAt, IsActive)
VALUES
(1, 'Michal', 'Kamyk', 'mk@test.com','#####','123456789', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', null, 'Admin', '2025-01-15 10:00:00', '2025-02-14 10:00:00', 1),
(2, 'Michal', 'Mufinka', 'mf@test.com','!!!!!','123321123', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', null, 'Worker', '2025-02-15 10:00:00', '2025-03-14 10:00:00', 1),
(3, 'Michal', 'Pizza', 'mp@test.com','@@@@@','420420420', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', null, 'Customer', '2025-03-15 10:00:00', '2025-04-14 10:00:00', 1),
(4, 'Michal', 'Boss', 'mb@test.com','*****','696969696', 'POL', 'Gliwice', '69-420', 'Akademicka', '16', null, 'Manager', '2025-03-15 10:00:00', '2025-04-14 10:00:00', 1);
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

INSERT INTO RepairsObjectsTypes (RepairObjectTypeId, Name)
VALUES
('CAR', 'Typical car'),
('PC', 'Personal Computer');

SET IDENTITY_INSERT RepairsObjects ON;
INSERT INTO RepairsObjects (RepairObjectId, Name, RepairObjectTypeId, CustomerId)
VALUES 
(1, 'Green fast car', 'CAR', 3),
(2, 'Gaming but not new computer', 'PC', 3);
SET IDENTITY_INSERT RepairsObjects OFF;

SET IDENTITY_INSERT RepairsRequests ON
INSERT INTO RepairsRequests (RepairRequestId, Description, Result, Status, RepairObjectId, ManagerId, IsPaid, CreatedAt, StartedAt, FinishedAt)
VALUES
(1, 'Looked into the engine', 'Something was wrong.', 'FIN', 1, 4, 1, '2025-03-17 10:00:00', '2025-03-17 12:00:00', '2025-03-17 15:30:00'),
(2, 'Looked into the engine', 'There were not the engine gasket', 'FIN', 1, 4, 1, '2025-03-18 11:00:00', '2025-03-18 12:12:12', '2025-03-18 14:30:00'),
(3, 'Looked into the motherboard', 'There was everything burned', 'FIN', 2, 4, 1, '2025-04-16 10:00:00', '2025-04-16 17:00:00', '2025-04-20 10:00:00');
SET IDENTITY_INSERT RepairsRequests OFF

INSERT INTO RepairsActivitiesTypes (RepairActivityTypeId, Name)
VALUES
('MOTHERBOARD', 'Motherboard changing'),
('OIL', 'Adding new oil'),
('GASKET', 'Replacement the gasket');

SET IDENTITY_INSERT RepairsActivities ON;
INSERT INTO RepairsActivities (RepairActivityId, RepairActivityTypeId, SequenceNumber, Description, Result, Status, RepairRequestId, WorkerId, CreatedAt, StartedAt, FinishedAt)
VALUES
(1, 'OIL', 1, 'We have added the new oil.', 'New oil added.', 'FIN', 1, 2, '2025-03-17 10:00:00', '2025-03-17 12:00:00', '2025-03-17 15:30:00'),
(2, 'GASKET', 2, 'Changing the gasket.', 'New gasket bought and replaced.', 'FIN', 1, 2, '2025-03-18 11:00:00', '2025-03-18 12:12:12', '2025-03-18 14:30:00'),
(3, 'MOTHERBOARD', 1, 'We have bought a new motherboard.', 'PC now works', 'FIN', 2, 2, '2025-04-16 10:00:00', '2025-04-16 17:00:00', '2025-04-20 10:00:00');
SET IDENTITY_INSERT RepairsActivities OFF;

*/
// * EXAMPLE SQL QUERY TO FILL OUR DATABASE *
