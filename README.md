# Bookify - Online Travel Agency

Bookify is an initial scaffolding for a scalable online travel agency (OTA) project focused on accommodation bookings. The project is structured as a modern distributed application with a .NET Core 8 backend, Keycloak authentication, PostgreSQL database, Nginx proxy for request management, and a React frontend.

This project uses a well-organized structure that separates the frontend (`client`), backend (`server`), and local dependencies (`local_deps`), facilitating development and deployment in different environments.

![Bookify](https://bookify-bay-tau.vercel.app/assets/bookify_logo-BrO1lyb6.png)

## Live Demo

You can see a live demo of the project at the following address:
[https://bookify-bay-tau.vercel.app/](https://bookify-bay-tau.vercel.app/)

## System Architecture

### Backend

The backend consists of several containerized services:

- **.NET Core 8 API**: Main service exposing RESTful APIs
- **Keycloak**: Identity and access management system
- **PostgreSQL**: Relational database for data persistence
- **Nginx**: Reverse proxy for request management and CORS

### Frontend

- **React**: Single Page Application (SPA) consuming backend APIs

### Infrastructure

- **Backend**: Services hosted on Railway via Dockerfile
- **Frontend**: Application hosted on Vercel

## Project Structure

```
.
├── .github/               # GitHub configurations
├── client/                # React Frontend
├── local_deps/            # Local dependencies for development
│   ├── docker-compose.yml # Docker Compose configuration
│   ├── Dockerfile.nginx   # Dockerfile for Nginx
│   ├── nginx/             # Nginx configuration
│   │   ├── nginx.conf     # Main configuration
│   │   └── default.conf   # Routing configuration
│   ├── init-db/           # Database initialization scripts
│   │   └── init-databases.sql
│   ├── keycloak/          # Keycloak configuration
│   │   └── realm-export/  # Realm configuration files
│   │       └── bookify-realm.json
│   └── README.md          # README for local dependencies
├── proxy/                 # Proxy configuration for production
├── server/                # .NET Core 8 Backend
│   ├── Bookify.csproj
│   ├── Controllers/
│   ├── Models/
│   └── Dockerfile
└── README.md              # This main file
```

## Main Features

- **Authentication and Authorization**: User management via Keycloak
- **RESTful API**: Endpoints for booking and accommodation management
- **Reverse Proxy**: Advanced CORS management and request routing
- **Reactive UI**: Modern and responsive user interface in React

## API Documentation

The Swagger documentation for the backend APIs is available at:

[https://proxy-production-197b.up.railway.app/swagger/index.html](https://proxy-production-197b.up.railway.app/swagger/index.html)

This interactive documentation allows you to explore and test all available REST endpoints, view data models, and understand how the API works.

## Requirements

- Docker and Docker Compose
- .NET SDK 8.0 (for local development)
- Node.js and npm (for frontend development)

## Local Setup

### 1. Clone the Repository

```bash
git clone https://github.com/yourusername/bookify.git
cd bookify
```

### 2. Start Services with Docker Compose

```bash
cd local_deps
docker-compose up -d
```

This command will start:

- PostgreSQL on port 5432
- Keycloak on port 8080
- Nginx on port 8081

### 3. Access the Keycloak Console

- URL: http://localhost:8080
- Username: admin
- Password: admin

### 4. Start the .NET Core Backend (Development)

```bash
cd server
dotnet run
```

The API service will be available at https://localhost:7276/api/

### 5. Start the React Frontend (Development)

```bash
cd client
npm install
npm run dev
```

The frontend will be available at http://localhost:5173

## Environment Configuration

### Environment Variables for Backend

```
ASPNETCORE_URLS=http://+:5000
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=Server=postgres;Port=5432;Database=bookify;User Id=postgres;Password=mysecretpassword;
```

### Nginx Configuration

The Nginx proxy is configured to:

- Forward API requests to the .NET Core backend
- Properly handle CORS headers
- Support WebSockets for real-time applications

For local development, it's configured to forward:

- API requests (/api/) to https://host.docker.internal:7276/api/
- Frontend requests (/) to http://host.docker.internal:5173

## Deployment

### Backend on Railway

The backend services (.NET Core API, PostgreSQL, Keycloak, Nginx) are hosted on Railway via their respective Dockerfiles.

### Frontend on Vercel

The React frontend application is hosted on Vercel.

## Railway Private Network

The project uses Railway's private network for communication between services, using internal domains like `bookify.railway.internal` for more secure and efficient communication.

## License

[MIT](https://choosealicense.com/licenses/mit/)

## Contacts

Project Name: [Bookify](https://bookify-bay-tau.vercel.app/)
