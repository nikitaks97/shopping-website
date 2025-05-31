# Use the official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY shopping-website.csproj ./
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Build the application
RUN dotnet publish -c Release -o out

# Use the official ASP.NET runtime image for the final container
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .
COPY --from=build /app/shoppingwebsite.db .

# Expose port 80 (standard ASP.NET Core port in containers)
EXPOSE 80

# Set the entrypoint
ENTRYPOINT ["dotnet", "shopping-website.dll"]
