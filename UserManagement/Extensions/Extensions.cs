using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using UserManagement.Services.Interfaces;
using UserManagement.Services.Services;

namespace UserManagement.API.Extensions
{
        public static class Extensions
        {
            public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
            {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IExpenseService, ExpenseService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            return services;
            }
        }
}