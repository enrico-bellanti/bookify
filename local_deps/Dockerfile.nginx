FROM nginx:alpine

# Rimuove la configurazione predefinita
RUN rm /etc/nginx/conf.d/default.conf

# Copia la configurazione personalizzata di Nginx
COPY nginx/nginx.conf /etc/nginx/nginx.conf
COPY nginx/default.conf /etc/nginx/conf.d/

# Espone la porta 8080
EXPOSE 8080

# Avvia Nginx in foreground
CMD ["nginx", "-g", "daemon off;"]