#!/bin/bash
# entrypoint.sh

set -e

# Wait for SQL Server to be ready
echo "Waiting for SQL Server to be ready..."
sleep 15

# Run migrations
echo "Running database migrations..."
cd /app
dotnet ef database update --project /app

# Start the application
echo "Starting application..."
exec dotnet Bookify.dll