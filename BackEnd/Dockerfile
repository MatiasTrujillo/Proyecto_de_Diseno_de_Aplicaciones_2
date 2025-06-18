# Build stage: Use the official .NET 8 SDK image
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory inside the container
WORKDIR /src

# Copy the project files to the container
COPY . .

# Restore dependencies
RUN dotnet restore "src/Simulator.WebApi/Simulator.WebApi.csproj"

# Build the application
RUN dotnet publish "src/Simulator.WebApi/Simulator.WebApi.csproj" -c Release -o /app/publish

# Runtime stage: Use the official .NET 8 runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Set the working directory for the runtime container
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /app/publish .

# Expose the port the application runs on
EXPOSE 80

# Set the entry point for the container
CMD ["dotnet", "Simulator.WebApi.dll"]