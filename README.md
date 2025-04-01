# Nginx Proxy per Railway

Questo repository contiene una configurazione Nginx ottimizzata per funzionare come proxy inverso su Railway, gestendo il CORS e inoltrando le richieste a un servizio backend interno.

## Struttura del Progetto

```
.
├── Dockerfile
├── docker-compose.yml
└── nginx/
    ├── nginx.conf
    └── default.conf
```

## Funzionalità

- Proxy inverso verso un servizio backend interno (`bookify.railway.internal:5000`)
- Gestione degli header CORS per consentire richieste cross-origin
- Gestione corretta delle richieste preflight OPTIONS
- Container leggero basato su Alpine Linux

## Deployment su Railway

1. Clona questo repository
2. Connettilo a Railway
3. Configura le variabili d'ambiente se necessario
4. Railway costruirà e deployerà automaticamente il servizio Nginx

## Configurazione CORS

Il proxy è configurato per permettere richieste da qualsiasi origine (`*`). Per limitare l'accesso a origini specifiche, modifica il valore di `Access-Control-Allow-Origin` nel file `nginx/default.conf`.

## Uso della Rete Privata Railway

Questo proxy utilizza la rete privata di Railway per comunicare con altri servizi, utilizzando nomi di dominio interni come `bookify.railway.internal`. Ciò garantisce una comunicazione più sicura ed efficiente tra i servizi.

## Personalizzazioni

Se hai bisogno di configurazioni più specifiche:

- Modifica `nginx/default.conf` per cambiare il routing o aggiungere location
- Aggiorna il Dockerfile se hai bisogno di funzionalità aggiuntive
- Personalizza gli header CORS per i tuoi requisiti specifici
