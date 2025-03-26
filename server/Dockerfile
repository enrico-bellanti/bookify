# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
RUN dotnet restore "./Bookify.csproj" --disable-parallel
RUN dotnet publish "./Bookify.csproj" -c release -o /app --no-restore

# Install EF Core tools in the build image
RUN dotnet tool install --global dotnet-ef

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
COPY entrypoint.sh /app/entrypoint.sh
RUN chmod +x /app/entrypoint.sh

# Configure ASP.NET Core to use the correct URLs
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_HTTP_PORT=5000

EXPOSE 5000

ENTRYPOINT ["/app/entrypoint.sh"]