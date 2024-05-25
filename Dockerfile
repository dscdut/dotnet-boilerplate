# Base image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /app

# Copy the solution file
COPY *.sln .

# Copy the project files
COPY DotnetBoilerplate.Api/*.csproj ./DotnetBoilerplate.Api/
COPY DotnetBoilerplate.Application/*.csproj ./DotnetBoilerplate.Application/
COPY DotnetBoilerplate.Domain/*.csproj ./DotnetBoilerplate.Domain/
COPY DotnetBoilerplate.Infrastructure/*.csproj ./DotnetBoilerplate.Infrastructure/
# Add more COPY statements for each project in your solution

# Restore NuGet packages
RUN dotnet restore "DotnetBoilerplate.Api/DotnetBoilerplate.Api.csproj"

# Copy the source code
COPY . .

WORKDIR /app/DotnetBoilerplate.Api
RUN dotnet build "DotnetBoilerplate.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish --no-restore -c Release -o /app/publish

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
ENV ASPNETCORE_HTTP_PORTS=3002
EXPOSE 3002
WORKDIR /app

COPY --from=publish /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "DotnetBoilerplate.Api.dll"]