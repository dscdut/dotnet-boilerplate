### To start local development with docker:
```		
docker compose up -d --build
```

### To install dotnet ef:
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