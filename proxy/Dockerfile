# Stage 1: Build .NET application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY *.sln .
COPY **/*.csproj ./
RUN for file in $(find . -name "*.csproj"); do mkdir -p "${file%/*}/" && mv "$file" "${file%/*}/"; done
RUN dotnet restore

# Copy everything else and build
COPY . .
RUN dotnet publish -c Release -o out

# Stage 2: Runtime environment with Nginx
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Install Nginx
RUN apt-get update && \
    apt-get install -y nginx && \
    apt-get clean && \
    rm -rf /var/lib/apt/lists/*

# Copy .NET runtime app
COPY --from=build /app/out ./

# Copy Nginx configuration
COPY nginx/nginx.conf /etc/nginx/nginx.conf
COPY nginx/default.conf /etc/nginx/conf.d/default.conf

# Set up startup script
COPY nginx/start.sh /start.sh
RUN chmod +x /start.sh

# Expose port
EXPOSE 8080

# Start both services
ENTRYPOINT ["/start.sh"]