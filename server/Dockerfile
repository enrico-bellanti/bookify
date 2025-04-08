# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source
COPY . .

# Restore dependencies
RUN dotnet restore "./Bookify.csproj" --disable-parallel

# Install EF Tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Publish the application
RUN dotnet publish "./Bookify.csproj" -c release -o /app --no-restore

# Serve Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./

# Copy migrations explicitly
COPY --from=build /source/Migrations /app/Migrations

# Environment variables
ARG ASPNETCORE_URLS
ARG ASPNETCORE_ENVIRONMENT
ARG ConnectionStrings__DefaultConnection

ENV ASPNETCORE_URLS=$ASPNETCORE_URLS
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT
ENV ConnectionStrings__DefaultConnection=$ConnectionStrings__DefaultConnection

EXPOSE 5000

ENTRYPOINT ["dotnet", "Bookify.dll"]