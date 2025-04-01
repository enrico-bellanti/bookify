# bookify

# .NET API con Nginx per la Gestione CORS

Questo progetto include un'applicazione .NET con un proxy Nginx integrato per gestire le richieste CORS.

## Struttura della Directory

```
.
├── Dockerfile
├── docker-compose.yml
├── nginx/
│   ├── nginx.conf
│   ├── default.conf
│   └── start.sh
└── ... (file del progetto .NET)
```

## Configurazione

### Modificare l'Applicazione .NET

1. **Assicurati di aggiornare il nome dell'applicazione** nel file `nginx/start.sh`:

   ```bash
   dotnet /app/YourApplication.dll --urls=http://localhost:5000
   ```

   Sostituisci `YourApplication.dll` con il nome corretto del tuo assembly.

2. **Configura l'URL di origine CORS** in `nginx/default.conf`:
   ```nginx
   add_header 'Access-Control-Allow-Origin' 'http://tuodominio.com' always;
   ```
   Sostituisci `'*'` con il dominio specifico da cui desideri consentire le richieste.

## Deployment

### Locale con Docker Compose

```bash
docker-compose up -d
```

### Su Railway

1. Connetti il repository a Railway
2. Railway utilizzerà automaticamente il Dockerfile per costruire e deployare l'applicazione
3. Configura le variabili d'ambiente in Railway se necessario

## Note Tecniche

- L'applicazione .NET espone internamente la porta 5000
- Nginx ascolta sulla porta 8080 e inoltra le richieste all'applicazione .NET
- Nginx gestisce tutti gli header CORS necessari
- Le richieste OPTIONS vengono gestite correttamente per le richieste preflight CORS
