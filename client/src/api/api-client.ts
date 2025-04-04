// src/api/apiClient.ts
import { useAuth } from '../shared/components/auth/AuthContext';
import { AccommodationApi, AddressApi, Configuration, TestApi } from './src/api';

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || "http://localhost:7276";

// Versione base senza autenticazione
export function createApiClient(basePath = API_BASE_URL) {
    const config = new Configuration({
        basePath: basePath
    });

    return {
        accommodation: new AccommodationApi(config),
        address: new AddressApi(config),
        test: new TestApi(config)
        // altri client API...
    };
}

// Versione con autenticazione
export function createAuthenticatedApiClient(accessToken?: string, basePath = API_BASE_URL) {
    const config = new Configuration({
        basePath: basePath,
        accessToken: accessToken,
    });

    return {
        accommodation: new AccommodationApi(config),
        address: new AddressApi(config),
        test: new TestApi(config)
        // altri client API...
    };
}

// Hook personalizzato per accedere all'API autenticata
export function useApiClient() {
    const auth = useAuth();
    const accessToken = auth.getAccessToken();

    return createAuthenticatedApiClient(accessToken);
}