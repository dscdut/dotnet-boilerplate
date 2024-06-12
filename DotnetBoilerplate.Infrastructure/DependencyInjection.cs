using Microsoft.Extensions.DependencyInjection;
using DotnetBoilerplate.Application.ExternalServices;
using DotnetBoilerplate.Application.Repositories;
using DotnetBoilerplate.Infrastructure.ExternalServices;
using DotnetBoilerplate.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using DotnetBoilerplate.Domain.Enums;
using DotnetBoilerplate.Infrastructure.Authorization;

namespace DotnetBoilerplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                if (string.IsNullOrEmpty(connectionString))
                {
                    connectionString = configuration.GetConnectionString("WebApiDatabase");
                }
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();


            services.AddScoped<IAuthorizationHandler, RolesHandler>();
            services.AddScoped<IAuthorizationHandler, MyAdminInfoHandler>();

            services.AddAuthorizationBuilder()
                .AddPolicy(PolicyName.CanDeleteUserPolicy, policy => policy.Requirements.Add(new RolesRequirement([RoleEnum.Admin], "The user is not authorized to delete information")))
                .AddPolicy(PolicyName.CanAdminUpdateUserPolicy, policy =>
                {
                    policy.Requirements.Add(new RolesRequirement([RoleEnum.Admin], "The user is not authorized to edit information"));
                    policy.Requirements.Add(new MyAdminInfoRequirement("The admin is not authorized to edit information of other admins"));
                });
            return services;
        }
    }
}
