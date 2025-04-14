# Bookify - Online Travel Agency

Bookify è uno scaffolding iniziale per un progetto scalabile di un'agenzia di viaggi online (OTA) focalizzata sulla prenotazione di alloggi. Il progetto è strutturato come un'applicazione distribuita moderna con un backend in .NET Core 8, autenticazione tramite Keycloak, database PostgreSQL, un proxy Nginx per la gestione delle richieste e un frontend in React.

Questo progetto utilizza una struttura ben organizzata che separa il frontend (`client`), il backend (`server`) e le dipendenze locali (`local_deps`), facilitando lo sviluppo e il deployment in ambienti diversi.

![Bookify](https://bookify-bay-tau.vercel.app/assets/bookify_logo-BrO1lyb6.png)

## Demo Live

È possibile vedere una demo live del progetto al seguente indirizzo:
[https://bookify-bay-tau.vercel.app/](https://bookify-bay-tau.vercel.app/)

## Architettura del Sistema

### Backend

Il backend è composto da diversi servizi containerizzati:

- **API .NET Core 8**: Servizio principale che espone le API RESTful
- **Keycloak**: Sistema di gestione delle identità e degli accessi
- **PostgreSQL**: Database relazionale per la persistenza dei dati
- **Nginx**: Proxy inverso per la gestione delle richieste e CORS

### Frontend

- **React**: Applicazione SPA (Single Page Application) che consuma le API del backend

### Infrastruttura

- **Backend**: Servizi ospitati su Railway tramite Dockerfile
- **Frontend**: Applicazione ospitata su Vercel

## Struttura del Progetto

```
.
├── .github/               # Configurazioni GitHub
├── client/                # Frontend React
├── local_deps/            # Dipendenze locali per sviluppo
│   ├── docker-compose.yml # Configurazione Docker Compose
│   ├── Dockerfile.nginx   # Dockerfile per Nginx
│   ├── nginx/             # Configurazione Nginx
│   │   ├── nginx.conf     # Configurazione principale
│   │   └── default.conf   # Configurazione di routing
│   ├── init-db/           # Script di inizializzazione database
│   │   └── init-databases.sql
│   ├── keycloak/          # Configurazione Keycloak
│   │   └── realm-export/  # File di configurazione del realm
│   │       └── bookify-realm.json
│   └── README.md          # README per dipendenze locali
├── proxy/                 # Configurazione proxy per produzione
├── server/                # Backend .NET Core 8
│   ├── Bookify.csproj
│   ├── Controllers/
│   ├── Models/
│   └── Dockerfile
└── README.md              # Questo file principale
```

## Funzionalità Principali

- **Autenticazione e Autorizzazione**: Gestione degli utenti tramite Keycloak
- **API RESTful**: Endpoints per la gestione delle prenotazioni e degli alloggi
- **Proxy Inverso**: Gestione avanzata del CORS e instradamento delle richieste
- **UI Reattiva**: Interfaccia utente moderna e responsive in React

## API Documentation

La documentazione Swagger per le API del backend è disponibile all'indirizzo:

[https://proxy-production-197b.up.railway.app/swagger/index.html](https://proxy-production-197b.up.railway.app/swagger/index.html)

Questa documentazione interattiva permette di esplorare e testare tutti gli endpoint REST disponibili, visualizzare i modelli di dati e comprendere il funzionamento dell'API.

## Requisiti

- Docker e Docker Compose
- .NET SDK 8.0 (per sviluppo locale)
- Node.js e npm (per sviluppo frontend)

## Setup Locale

### 1. Clonare il Repository

```bash
git clone https://github.com/tuousername/bookify.git
cd bookify
```

### 2. Avviare i Servizi con Docker Compose

```bash
cd local_deps
docker-compose up -d
```

Questo comando avvierà:

- PostgreSQL sulla porta 5432
- Keycloak sulla porta 8080
- Nginx sulla porta 8081

### 3. Accedere alla Console di Keycloak

- URL: http://localhost:8080
- Username: admin
- Password: admin

### 4. Avviare il Backend .NET Core (Sviluppo)

```bash
cd server
dotnet run
```

Il servizio API sarà disponibile su https://localhost:7276/api/

### 5. Avviare il Frontend React (Sviluppo)

```bash
cd client
npm install
npm run dev
```

Il frontend sarà disponibile su http://localhost:5173

## Configurazione Ambiente

### Variabili d'Ambiente per il Backend

```
ASPNETCORE_URLS=http://+:5000
ASPNETCORE_ENVIRONMENT=Development
ConnectionStrings__DefaultConnection=Server=postgres;Port=5432;Database=bookify;User Id=postgres;Password=mysecretpassword;
```

### Configurazione Nginx

Il proxy Nginx è configurato per:

- Inoltrare le richieste API al backend .NET Core
- Gestire correttamente gli header CORS
- Supportare WebSockets per le applicazioni in tempo reale

Per lo sviluppo locale, è configurato per inoltrare:

- Le richieste API (/api/) a https://host.docker.internal:7276/api/
- Le richieste frontend (/) a http://host.docker.internal:5173

## Deployment

### Backend su Railway

I servizi backend (API .NET Core, PostgreSQL, Keycloak, Nginx) sono ospitati su Railway tramite i rispettivi Dockerfile.

### Frontend su Vercel

L'applicazione frontend React è ospitata su Vercel.

## Rete Privata Railway

Il progetto utilizza la rete privata di Railway per la comunicazione tra servizi, usando domini interni come `bookify.railway.internal` per una comunicazione più sicura ed efficiente.

### Database

Gli script di inizializzazione del database si trovano nella directory `local_deps/init-db/` e vengono eseguiti automaticamente durante il primo avvio del container PostgreSQL. Lo script `init-databases.sql` crea i database necessari per Keycloak e Bookify.

## Licenza

[MIT](https://choosealicense.com/licenses/mit/)

## Contatti

Nome Progetto: [Bookify](https://bookify-bay-tau.vercel.app/)
