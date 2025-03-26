# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .
# RUN ls -la  # <-- Aggiungi questo per debug, vedrai i file copiati
RUN dotnet restore "./Bookify.csproj" --disable-parallel
RUN dotnet publish "./Bookify.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Configure ASP.NET Core to use the correct URLs
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Development
# ENV ASPNETCORE_URLS=http://+:5000;https://+:5001
ENV ASPNETCORE_HTTP_PORT=5000
# ENV ASPNETCORE_HTTPS_PORT=5001
# Development certificate for HTTPS in container
# ENV ASPNETCORE_Kestrel__Certificates__Default__Path=/app/cert/cert.pfx
# ENV ASPNETCORE_Kestrel__Certificates__Default__Password=YourSecurePassword

# Create directory for the cert (will be mounted from host)
# RUN mkdir -p /app/cert

EXPOSE 5000
# EXPOSE 5001

ENTRYPOINT ["dotnet", "Bookify.dll"]