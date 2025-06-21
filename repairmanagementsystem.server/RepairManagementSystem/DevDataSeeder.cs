using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RepairManagementSystem.Data;
using RepairManagementSystem.Models;
using RepairManagementSystem.Repositories.Interfaces;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem
{
    public static class DevDataSeeder
    {
        public static async Task SeedAdminUserAsync(IServiceProvider services)
        {
            var userRepo = services.GetRequiredService<IUserRepository>();
            var crypto = services.GetRequiredService<ICryptoService>();
            var context = services.GetRequiredService<ApplicationDbContext>();
            var repairObjectTypeRepo = services.GetRequiredService<IRepairObjectTypeRepository>();
            var repairObjectRepo = services.GetRequiredService<IRepairObjectRepository>();
            var repairRequestRepo = services.GetRequiredService<IRepairRequestRepository>();
            var repairActivityRepo = services.GetRequiredService<IRepairActivityRepository>();
            var customerRepo = services.GetRequiredService<ICustomerRepository>();
            var managerRepo = services.GetRequiredService<IManagerRepository>();
            var repairActivityTypeRepo = services.GetRequiredService<IRepairActivityTypeRepository>();

            var (hash, salt) = crypto.HashPassword("admin12345");
            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "User",
                Email = "admin@tab.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Number = "123456789",
                Address = new Address
                {
                    Country = "PL",
                    City = "TabCity",
                    PostalCode = "00-001",
                    Street = "Tab Street",
                    HouseNumber = "1",
                    ApartNumber = "1A"
                },
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };
            await userRepo.AddUserAsync(adminUser);

            var customer = new User
            {
                FirstName = "Customer1",
                LastName = "User",
                Email = "customer1@tab.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Number = "123456789",
                Address = new Address
                {
                    Country = "PL",
                    City = "TabCity",
                    PostalCode = "00-001",
                    Street = "Tab Street",
                    HouseNumber = "1",
                    ApartNumber = "1A"
                },
                Role = "Customer",
                CreatedAt = DateTime.UtcNow
            };
            await userRepo.AddUserAsync(customer);

            var customer3 = new User
            {
                FirstName = "Customer2",
                LastName = "User",
                Email = "customer2@tab.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Number = "123456789",
                Address = new Address
                {
                    Country = "PL",
                    City = "TabCity",
                    PostalCode = "00-001",
                    Street = "Tab Street",
                    HouseNumber = "1",
                    ApartNumber = "1A"
                },
                Role = "Customer",
                CreatedAt = DateTime.UtcNow
            };
            await userRepo.AddUserAsync(customer3);

            // Add Customer objects
            var customerEntity1 = new Customer
            {
                UserId = customer.UserId,
                User = customer,
                PaymentMethod = "CreditCard"
            };
            var customerEntity2 = new Customer
            {
                UserId = customer3.UserId,
                User = customer3,
                PaymentMethod = "Cash"
            };
            await customerRepo.AddCustomerAsync(customerEntity1);
            await customerRepo.AddCustomerAsync(customerEntity2);

            var managerUser = new User
            {
                FirstName = "Manager",
                LastName = "User",
                Email = "manager@tab.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Number = "123456789",
                Address = new Address
                {
                    Country = "PL",
                    City = "TabCity",
                    PostalCode = "00-001",
                    Street = "Tab Street",
                    HouseNumber = "1",
                    ApartNumber = "1A"
                },
                Role = "Manager",
                CreatedAt = DateTime.UtcNow
            };
            await userRepo.AddUserAsync(managerUser);

            var workerUser1 = new User
            {
                FirstName = "Worker1",
                LastName = "User",
                Email = "worker@tab.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Number = "123456789",
                Address = new Address
                {
                    Country = "PL",
                    City = "TabCity",
                    PostalCode = "00-001",
                    Street = "Tab Street",
                    HouseNumber = "1",
                    ApartNumber = "1A"
                },
                Role = "Worker",
                CreatedAt = DateTime.UtcNow
            };
            await userRepo.AddUserAsync(workerUser1);

            var workerUser2 = new User
            {
                FirstName = "Worker2",
                LastName = "User",
                Email = "worker@tab.com",
                PasswordHash = hash,
                PasswordSalt = salt,
                Number = "123456789",
                Address = new Address
                {
                    Country = "PL",
                    City = "TabCity",
                    PostalCode = "00-001",
                    Street = "Tab Street",
                    HouseNumber = "1",
                    ApartNumber = "1A"
                },
                Role = "Worker",
                CreatedAt = DateTime.UtcNow
            };
            await userRepo.AddUserAsync(workerUser2);

            // Add Worker objects
            var workerEntity1 = new Worker
            {
                UserId = workerUser1.UserId,
                User = workerUser1,
                Expertise = "Electronics",
                IsAvailable = true
            };
            var workerEntity2 = new Worker
            {
                UserId = workerUser2.UserId,
                User = workerUser2,
                Expertise = "Mechanics",
                IsAvailable = true
            };
            var workerRepo = services.GetRequiredService<IWorkerRepository>();
            await workerRepo.AddWorkerAsync(workerEntity1);
            await workerRepo.AddWorkerAsync(workerEntity2);

            // Add RepairObjectTypes
            var type1 = new RepairObjectType { RepairObjectTypeId = "ELE", Name = "Elektryczny" };
            var type2 = new RepairObjectType { RepairObjectTypeId = "MECH", Name = "Mechaniczny" };
            await repairObjectTypeRepo.AddRepairObjectTypeAsync(type1);
            await repairObjectTypeRepo.AddRepairObjectTypeAsync(type2);

            // Get customers and manager
            var customers = (await customerRepo.GetAllCustomersAsync())?.ToList();
            var customer1 = customers != null && customers.Count > 0 ? customers[0] : null;
            var customer2 = customers != null && customers.Count > 1 ? customers[1] : null;
            var managers = (await managerRepo.GetAllManagersAsync())?.ToList();
            var manager = managers != null && managers.Count > 0 ? managers[0] : null;

            // Add RepairObjects
            var obj1 = new RepairObject { Name = "Komputer", RepairObjectTypeId = "ELE", RepairObjectType = type1, CustomerId = customer1?.UserId ?? 1, Customer = customer1! };
            var obj2 = new RepairObject { Name = "Wiertarka", RepairObjectTypeId = "MECH", RepairObjectType = type2, CustomerId = customer2?.UserId ?? 2, Customer = customer2! };
            await repairObjectRepo.AddRepairObjectAsync(obj1);
            await repairObjectRepo.AddRepairObjectAsync(obj2);

            // Add Manager entity for the manager user
            var managerEntity = new Manager { UserId = managerUser.UserId, User = managerUser, Expertise = "General", ActiveRepairsCount = 1 };
            await managerRepo.AddManagerAsync(managerEntity);

            Manager? trackedManager = null;
            if (managerEntity != null)
            {
                trackedManager = await managerRepo.GetManagerByIdAsync(managerEntity.UserId);
            }

            // Add RepairRequests
            var req1 = new RepairRequest { Description = "Komputer nie działa", Result = "", Status = "NEW", RepairObjectId = obj1.RepairObjectId, RepairObject = obj1, ManagerId = null, IsPaid = false };
            var req2 = new RepairRequest { Description = "Wiertarka nie wierci", Result = "", Status = "OPEN", RepairObjectId = obj2.RepairObjectId, RepairObject = obj2, ManagerId = trackedManager?.UserId, IsPaid = false };
            if (trackedManager != null)
            {
                req2.Manager = trackedManager;
            }
            await repairRequestRepo.AddRepairRequestAsync(req1);
            await repairRequestRepo.AddRepairRequestAsync(req2);

            // Add RepairActivityType
            var activityType = new RepairActivityType { RepairActivityTypeId = "STEP", Name = "Step" };
            await repairActivityTypeRepo.AddRepairActivityTypeAsync(activityType);

            // Add RepairActivities (tasks)
            var act1 = new RepairActivity { name = "Check damages", RepairActivityTypeId = "STEP", RepairActivityType = activityType, SequenceNumber = 1, Description = "Initial check", Result = "", Status = "OPEN", RepairRequestId = req1.RepairRequestId };
            var act2 = new RepairActivity { name = "Check damages", RepairActivityTypeId = "STEP", RepairActivityType = activityType, SequenceNumber = 1, Description = "Initial check", Result = "", Status = "InProgress", RepairRequestId = req2.RepairRequestId };
            await repairActivityRepo.AddRepairActivityAsync(act1);
            await repairActivityRepo.AddRepairActivityAsync(act2);
        }
    }
}
