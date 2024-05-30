
using DotnetBoilerplate.Application.Mappings;
using DotnetBoilerplate.Application;
using DotnetBoilerplate.Domain;
using DotnetBoilerplate.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using DotnetBoilerplate.Infrastructure.Middleware;

namespace DotnetBoilerplate.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<DataContext>(options =>
            {
                string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");
                if (connectionString.IsNullOrEmpty())
                {
                    connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
                }
                options.UseNpgsql(connectionString);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetBoilerplate", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme." +
                                  "Enter 'Bearer' [space] and then your token in the text input below." +
                                  "Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                },
                                Scheme = "oauth2",
                                Name = "Bearer",
                                In = ParameterLocation.Header,

                            },
                            new List<string>()
                        }
                });
            });
            builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
            builder.Services.AddApplication(builder.Configuration);
            builder.Services.AddInfrastructure();
            var app = builder.Build();

            app.UseCors(builder =>
            {
                builder.WithOrigins(app.Configuration["AllowedHosts"])
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<CustomExceptionMiddleware>();
            app.MapControllers();
            app.Run();
        }
    }
}
