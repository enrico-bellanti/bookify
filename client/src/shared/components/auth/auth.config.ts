import { WebStorageStateStore } from "oidc-client-ts";

export const oidcConfig = {
    // authority: import.meta.env.VITE_KEYCLOAK_URL || "http://localhost:8080/realm/bookify",
    authority: (import.meta.env.VITE_KEYCLOAK_URL || "http://localhost:8080") +
        "/realms/" + (import.meta.env.VITE_KEYCLOAK_REALM || "master"),
    client_id: import.meta.env.VITE_KEYCLOAK_CLIENT_ID || "bookify-front-end",
    redirect_uri: import.meta.env.VITE_REDIRECT_URI || window.location.origin,
    response_type: "code",
    scope: import.meta.env.VITE_KEYCLOAK_SCOPE || "openid profile email",

    // Per persistere la sessione in localStorage
    userStore: new WebStorageStateStore({ store: window.localStorage }),

    // Importante: rimuovere i parametri di autenticazione dall'URL dopo il login
    onSigninCallback: () => {
        window.history.replaceState({}, document.title, window.location.pathname);
    }
};