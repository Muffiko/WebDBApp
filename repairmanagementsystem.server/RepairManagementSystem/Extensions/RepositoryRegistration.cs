using RepairManagementSystem.Repositories;
using RepairManagementSystem.Repositories.Interfaces;
namespace RepairManagementSystem.Extensions
{
    public static class RepositoryRegistration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
    }
}
