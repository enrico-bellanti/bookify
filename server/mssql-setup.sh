#!/bin/bash

# Download SQL Server package
curl -o mssql-server.deb https://packages.microsoft.com/ubuntu/22.04/mssql-server-2022/pool/main/m/mssql-server/mssql-server_16.0.4085.2-1_amd64.deb

# Install SQL Server package
dpkg -i mssql-server.deb || true

# Fix any broken dependencies
apt-get update
apt-get install -f -y

# Configure SQL Server
/opt/mssql/bin/mssql-conf setup accept-eula

# Start SQL Server
/opt/mssql/bin/sqlservr