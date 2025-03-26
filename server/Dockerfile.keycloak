# Usa l'immagine ufficiale di Keycloak
FROM quay.io/keycloak/keycloak:21.1.1

# Imposta variabili di ambiente per configurare Keycloak
ENV KEYCLOAK_ADMIN=admin
ENV KEYCLOAK_ADMIN_PASSWORD=admin
# ENV KC_DB=postgres
# ENV KC_DB_URL=jdbc:postgresql://keycloak-db:5432/keycloak
# ENV KC_DB_USERNAME=keycloak
# ENV KC_DB_PASSWORD=keycloak
ENV KC_HEALTH_ENABLED=true

# Crea una directory per i file di importazione dei realm
RUN mkdir -p /opt/keycloak/data/import

# Copia il file di esportazione del realm nella directory di importazione
COPY ./keycloak/realm-export /opt/keycloak/data/import

# Configura il comando per avviare Keycloak con l'importazione del realm
CMD ["start-dev", "--import-realm"]

# Espone la porta 8080 per accedere a Keycloak
EXPOSE 8080

# Configura il healthcheck per monitorare lo stato del container
HEALTHCHECK --interval=15s --timeout=10s --retries=15 --start-period=60s \
    CMD bash -c ':> /dev/tcp/127.0.0.1/8080' || exit 1
