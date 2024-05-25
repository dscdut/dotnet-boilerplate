using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using DotnetBoilerplate.Application;
using DotnetBoilerplate.Domain;
using DotnetBoilerplate.Application.Mappings;
using DotnetBoilerplate.Infrastructure.Middleware;
using DotnetBoilerplate.Infrastructure;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

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
builder.Services.AddEndpointsApiExplorer();
// Thêm dòng sau để thêm token JWT
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DotnetBoilerplate", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"JWT Authorization header using the Bearer scheme. \n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \nExample: 'Bearer 12345abcdef'",
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
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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


app.MapGet("/", async context =>
{
    await context.Response.WriteAsync("Hello World!");
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DotnetBoilerplate");
    c.InjectJavascript("/custom.js");
});
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseMiddleware<CustomExceptionMiddleware>();
app.MapControllers();
app.Run();
