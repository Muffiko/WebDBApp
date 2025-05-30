using RepairManagementSystem.Repositories;
using RepairManagementSystem.Repositories.Interfaces;
namespace RepairManagementSystem.Extensions
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IManagerRepository, ManagerRepository>();
            services.AddScoped<IRepairActivityRepository, RepairActivityRepository>();
            services.AddScoped<IRepairActivityTypeRepository, RepairActivityTypeRepository>();
            services.AddScoped<IRepairObjectRepository, RepairObjectRepository>();
            services.AddScoped<IRepairObjectTypeRepository, RepairObjectTypeRepository>();
            services.AddScoped<IRepairRequestRepository, RepairRequestRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            return services;
        }
    }
}
