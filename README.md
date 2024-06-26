# ASP.NET Core Web API Boilerplate

_DotnetBoilerplate_ is a **starter kit**. This project follows [Clean Architecture](https://juldhais.net/clean-architecture-in-asp-net-core-web-api-4e5ef0b96f99).

## Technologies

- ASP.NET Core Web API (.NET 8.0)
- Entity Framework Core (8.0.4)
- Fluent Validation
- JwtBearer Authentication
- PostgreSQL
- AutoMapper
- Swagger (Swashbuckle)
- MailKit

## Folder Structure Conventions

    .
    ├── DotnetBoilerplate.Domain
    │   ├── Entities # Store entity mapping with table in database
    │   ├── Enums 
    │   └── Payloads # Object to store data that specific to project
    ├── DotnetBoilerplate.Application
    │   ├── Dtos # Mostly use when receive data and return data to client
    │   ├── Exceptions
    │   ├── ExternalSerivces # Interfaces of external services like Email, Upload ...
    │   ├── Profiles # Configuration of some profiles like AutoMapper ...
    │   ├── Repositories # Interfaces of repositories(Data Access Layer) ...
    │   └── DependencyInjection.cs # Register Dependency Injection used in this layer
    ├── DotnetBoilerplate.Infrastructure
    │   ├── Authorization # Implement Authorization Policies
    │   ├── ExternalSerivces # Implement external services from 'Application Layer' ...
    │   ├── Repositories # Implement repositories from 'Application Layer'
    │   ├── Migrations
    │   ├── DataContext.cs
    │   └── DependencyInjection.cs
    ├── DotnetBoilerplate.Api
    │   ├── Controllers
    │   ├── Filters
    │   ├── Middlewares
    │   ├── Params # Common objects to store data from request
    │   ├── Validators # Implement validations for request data
    │   └── Program.cs
    ├── Dockerfile
    ├── docker-compose
    └── README.md

## Customization
- Create _appsettings.Development.json_ file in _DotnetBoilerplate.Api_ folder to store environment variables for development environment. All your configurations in _appsettings.json_ file will be overridden by _appsettings.Development.json_ file.

- You can customize `token information (secret key, expiry date) ` in [_appsettings.json_](DotnetBoilerplate.Api/appsettings.json) file.

```json
"JwtSettings": {
    "Secret": "This is a secret key for authentication",
    "AccessTokenExpirationTime": 120,
    "RefreshTokenExpirationTime": 10080
  }
```

- You can customize `database connection information` in [_appsettings.json_](DotnetBoilerplate.Api/appsettings.json) file.

```json
"ConnectionStrings": {
    "DefaultConnection": "Host=localhost; Port=5432; Database=dotnet-boilerplate; Username=postgres; Password=postgres"
  },
```

## Useful commands

### To start local development with docker:

```
docker compose up -d --build
```

### To just run database service on docker compose

```
docker compose up -d db
```

### To work with Entify Framework CLI, you can use _Package Manager_ of `Visual Studio / JetBrains Rider`, or you can install _dotnet ef_ tool:

```
dotnet tool install --global dotnet-ef --version 8.0.4

```

### To add migrations:

- dotnet cli

```
dotnet ef migrations add InitialCreate -p DotnetBoilerplate.Infrastructure\DotnetBoilerplate.Infrastructure.csproj -s DotnetBoilerplate.Api\DotnetBoilerplate.Api.csproj
```

- Package Manager Console

```
Add-Migration InitialCreate -p DotnetBoilerplate.Infrastructure -s DotnetBoilerplate.Api
```

### To update database:

- dotnet cli

```
dotnet ef database update -p DotnetBoilerplate.Infrastructure\DotnetBoilerplate.Infrastructure.csproj -s DotnetBoilerplate.Api\DotnetBoilerplate.Api.csproj
```

- Package Manager Console

```
Update-Database -p DotnetBoilerplate.Infrastructure -s DotnetBoilerplate.Api
```
