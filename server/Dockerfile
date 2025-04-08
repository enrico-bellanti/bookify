# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
# RUN ls -la  # <-- Aggiungi questo per debug, vedrai i file copiati
RUN dotnet restore "./Bookify.csproj" --disable-parallel
# Add EF Core tools and create migration
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="${PATH}:/root/.dotnet/tools"
# Accept the connection string as a build argument
ARG ConnectionStrings__DefaultConnection

# Set it as an environment variable with the same name
ENV ConnectionStrings__DefaultConnection=$ConnectionStrings__DefaultConnection
# ENV ConnectionStrings__DefaultConnection="Host=postgres-15.railway.internal;Port=5432;Database=railway;Username=postgres;Password=b6FFF4Gb2DA15ADCCae25EE1gfFEbbdb;SSL Mode=Require;Trust Server Certificate=true"
# RUN dotnet ef migrations add InitialCreate
# RUN dotnet ef database update

RUN dotnet publish "./Bookify.csproj" -c release -o /app --no-restore

# Install the EF tools
# RUN dotnet tool install --global dotnet-ef
# ENV PATH="${PATH}:/root/.dotnet/tools"
# RUN dotnet ef database update


# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

ARG ASPNETCORE_URLS
ARG ASPNETCORE_ENVIRONMENT
ARG ASPNETCORE_HTTP_PORT

ENV ASPNETCORE_URLS=$ASPNETCORE_URLS
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV ASPNETCORE_HTTP_PORT=$ASPNETCORE_HTTP_PORT
# Configure ASP.NET Core to use the correct URLs
# ENV ASPNETCORE_URLS=http://+:5000
# ENV ASPNETCORE_ENVIRONMENT=Development
# ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
# ENV ASPNETCORE_HTTP_PORT=5000
# ENV ASPNETCORE_HTTPS_PORT=5001
# Development certificate for HTTPS in container
# ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/cert/cert.pfx
# ENV ASPNETCORE_Kestrel__Certificates__Default__Password=YourSecurePassword

# Create directory for the cert (will be mounted from host)
# RUN mkdir -p /app/cert

EXPOSE 5000
# EXPOSE 5001

ENTRYPOINT ["dotnet", "Bookify.dll"]



# # Build Stage
# FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
# WORKDIR /source
# COPY . .
# RUN dotnet restore "./Bookify.csproj" --disable-parallel
# RUN dotnet publish "./Bookify.csproj" -c release -o /app --no-restore

# # Serve Stage
# FROM mcr.microsoft.com/dotnet/aspnet:8.0
# WORKDIR /app
# COPY --from=build /app ./

# ENV ASPNETCORE_URLS=http://+:5000
# ENV ASPNETCORE_ENVIRONMENT=Development
# ENV ASPNETCORE_HTTP_PORT=5000

# EXPOSE 5000

# ENTRYPOINT ["dotnet", "Bookify.dll"]