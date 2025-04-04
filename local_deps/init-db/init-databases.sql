-- Crea il database per Keycloak se non esiste
CREATE DATABASE keycloak;

-- Crea il database per Bookify se non esiste
CREATE DATABASE bookify;

-- Concedi tutti i privilegi all'utente postgres sui database
GRANT ALL PRIVILEGES ON DATABASE keycloak TO postgres;
GRANT ALL PRIVILEGES ON DATABASE bookify TO postgres;