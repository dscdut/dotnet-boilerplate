using Microsoft.Extensions.DependencyInjection;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Infrastructure.ExternalServices;
using DotnetBoilerplate.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace DotnetBoilerplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<VNPayService>();
            services.AddScoped<MoMoService>();

            services.AddScoped<Func<string, IPaymentService>>(serviceProvider => key =>
            {
                return key switch
                {
                    "VNPay" => serviceProvider.GetService<VNPayService>() as IPaymentService,
                    "MoMo" => serviceProvider.GetService<MoMoService>() as IPaymentService,
                    _ => throw new KeyNotFoundException($"Payment service not found for key: {key}")
                };
            });

            return services;
        }
    }
}
