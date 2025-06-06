using RepairManagementSystem.Services;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Extensions
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IRepairObjectService, RepairObjectService>();
            services.AddScoped<IRepairRequestService, RepairRequestService>();
            services.AddScoped<IRepairActivityService, RepairActivityService>();
            services.AddScoped<IRepairObjectTypeService, RepairObjectTypeService>();
            services.AddScoped<IRepairActivityTypeService, RepairActivityTypeService>();
            services.AddScoped<ICryptoService, CryptoService>();
            return services;
        }
    }
}
