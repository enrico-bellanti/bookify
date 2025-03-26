FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Install build dependencies
WORKDIR /source
COPY . .
RUN dotnet restore "./Bookify.csproj" --disable-parallel
RUN dotnet publish "./Bookify.csproj" -c release -o /app --no-restore

# Install EF tools for migrations
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Install SQL Server
RUN apt-get update && apt-get install -y \
    wget \
    apt-transport-https \
    software-properties-common \
    gnupg \
    curl \
    supervisor \
    postgresql \
    postgresql-contrib \
    openjdk-17-jdk

# Install SQL Server
RUN wget -qO- https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/22.04/mssql-server-2022.list)" && \
    apt-get update && \
    apt-get install -y mssql-server

# Configure SQL Server
RUN /opt/mssql/bin/mssql-conf set MSSQL_SA_PASSWORD "YourStrong!Passw0rd" && \
    /opt/mssql/bin/mssql-conf set ACCEPT_EULA Y && \
    /opt/mssql/bin/mssql-conf set MSSQL_PID Express

# Download Keycloak
RUN mkdir -p /opt/keycloak && \
    curl -L https://github.com/keycloak/keycloak/releases/download/21.1.1/keycloak-21.1.1.tar.gz | tar xz -C /opt/keycloak --strip-components=1

# Copy application files
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools

# Create directories
RUN mkdir -p /app/cert /opt/keycloak/data/import /var/lib/postgresql/data
RUN chmod 700 /var/lib/postgresql/data && chown -R postgres:postgres /var/lib/postgresql/data

# Copy Keycloak realm export
COPY ./keycloak/realm-export /opt/keycloak/data/import/

# Set up supervisor
COPY ./supervisord.conf /etc/supervisor/conf.d/supervisord.conf

# Set environment variables
ENV PATH="${PATH}:/root/.dotnet/tools" \
    ASPNETCORE_URLS=http://+:5000;https://+:5001 \
    ASPNETCORE_HTTP_PORT=5000 \
    ASPNETCORE_HTTPS_PORT=5001 \
    ASPNETCORE_Kestrel__Certificates__Default__Path=/app/cert/cert.pfx \
    ASPNETCORE_Kestrel__Certificates__Default__Password=YourSecurePassword \
    ConnectionStrings__DefaultConnection="Server=localhost;Database=BookifyDB;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True;" \
    ASPNETCORE_ENVIRONMENT=Development \
    Keycloak__BaseUrl=http://localhost:8080 \
    Keycloak__Realm=bookify \
    Keycloak__ClientId=bookify-back-end \
    Keycloak__Secret=fftGCkyRUJkbwUnBf3vB0f2jxEgPNBxo \
    KEYCLOAK_ADMIN=admin \
    KEYCLOAK_ADMIN_PASSWORD=admin \
    KC_DB=postgres \
    KC_DB_URL=jdbc:postgresql://localhost:5432/keycloak \
    KC_DB_USERNAME=keycloak \
    KC_DB_PASSWORD=keycloak \
    KC_HEALTH_ENABLED=true \
    POSTGRES_DB=keycloak \
    POSTGRES_USER=keycloak \
    POSTGRES_PASSWORD=keycloak

# Expose ports
EXPOSE 5000 5001 1433 8080 5432

# Start all services using supervisor
CMD ["/usr/bin/supervisord", "-c", "/etc/supervisor/conf.d/supervisord.conf"]