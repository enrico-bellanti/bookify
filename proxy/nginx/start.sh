#!/bin/bash

# Avvia il servizio Nginx in background
service nginx start

# Avvia l'applicazione .NET
dotnet /app/YourApplication.dll --urls=http://localhost:5000