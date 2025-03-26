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
FROM ubuntu:22.04

# Set non-interactive installation
ENV DEBIAN_FRONTEND=noninteractive

# Install dependencies
RUN apt-get update && apt-get install -y \
    wget \
    apt-transport-https \
    software-properties-common \
    gnupg \
    curl \
    supervisor \
    lsb-release \
    ca-certificates \
    sudo \
    unixodbc-dev \
    && rm -rf /var/lib/apt/lists/*

# Install PostgreSQL
RUN apt-get update && apt-get install -y postgresql postgresql-contrib \
    && rm -rf /var/lib/apt/lists/*

# Install .NET Runtime
RUN apt-get update && \
    apt-get install -y --no-install-recommends \
    wget \
    && rm -rf /var/lib/apt/lists/* \
    && wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb \
    && dpkg -i packages-microsoft-prod.deb \
    && rm packages-microsoft-prod.deb \
    && apt-get update \
    && apt-get install -y --no-install-recommends \
    dotnet-runtime-8.0 \
    aspnetcore-runtime-8.0 \
    && rm -rf /var/lib/apt/lists/*

# Install MSSQL Tools (for sqlcmd)
RUN curl https://packages.microsoft.com/keys/microsoft.asc | apt-key add - \
    && curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/prod.list > /etc/apt/sources.list.d/mssql-release.list \
    && apt-get update \
    && ACCEPT_EULA=Y apt-get install -y mssql-tools18 unixodbc-dev \
    && rm -rf /var/lib/apt/lists/* \
    && echo 'export PATH="$PATH:/opt/mssql-tools18/bin"' >> ~/.bashrc

# Install Java for Keycloak
RUN apt-get update && \
    apt-get install -y openjdk-17-jdk \
    && rm -rf /var/lib/apt/lists/*

# Download Keycloak
RUN mkdir -p /opt/keycloak && \
    curl -L https://github.com/keycloak/keycloak/releases/download/21.1.1/keycloak-21.1.1.tar.gz | tar xz -C /opt/keycloak --strip-components=1

# Install SQL Server (just the client, not the server)
RUN wget -qO- https://packages.microsoft.com/keys/microsoft.asc | apt-key add - && \
    curl https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/mssql-server-preview.list > /etc/apt/sources.list.d/mssql-server-preview.list && \
    apt-get update && \
    ACCEPT_EULA=Y apt-get install -y msodbcsql18 \
    && rm -rf /var/lib/apt/lists/*

# Copy application files
WORKDIR /app
COPY --from=build /app ./
COPY --from=build /root/.dotnet/tools /root/.dotnet/tools

# Download SQL Server Docker container
RUN mkdir -p /opt/mssql-docker && \
    curl -L https://github.com/microsoft/mssql-docker/archive/refs/heads/master.zip -o /opt/mssql-docker.zip && \
    apt-get update && \
    apt-get install -y unzip && \
    unzip /opt/mssql-docker.zip -d /opt && \
    rm /opt/mssql-docker.zip && \
    apt-get remove -y unzip && \
    rm -rf /var/lib/apt/lists/*

# Create directories
RUN mkdir -p /app/cert /opt/keycloak/data/import /var/lib/postgresql/data /var/opt/mssql
RUN chmod 700 /var/lib/postgresql/data && chown -R postgres:postgres /var/lib/postgresql/data
RUN chmod 755 /var/opt/mssql

# Copy Keycloak realm export
COPY ./keycloak/realm-export /opt/keycloak/data/import/

# Set up supervisor
COPY ./supervisord.conf /etc/supervisor/conf.d/supervisord.conf

# Set environment variables
ENV PATH="${PATH}:/root/.dotnet/tools:/opt/mssql-tools18/bin" \
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
    POSTGRES_PASSWORD=keycloak \
    ACCEPT_EULA=Y \
    MSSQL_SA_PASSWORD=YourStrong!Passw0rd \
    MSSQL_PID=Express

# Copy SQL Server setup script
COPY mssql-setup.sh /usr/local/bin/
RUN chmod +x /usr/local/bin/mssql-setup.sh

# Expose ports
EXPOSE 5000 5001 1433 8080 5432

# Start all services using supervisor
CMD ["/usr/bin/supervisord", "-c", "/etc/supervisor/conf.d/supervisord.conf"]