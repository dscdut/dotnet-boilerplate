using Microsoft.Extensions.DependencyInjection;
using DotnetBoilerplate.Application.Services;

namespace DotnetBoilerplate.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            //services.AddScoped<IUserService, UserService>();

            return services;
        }
    }
}
