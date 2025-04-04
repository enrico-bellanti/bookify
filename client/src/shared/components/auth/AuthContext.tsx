// src/auth/AuthContext.tsx
import { User, UserProfile } from "oidc-client-ts";
import { createContext, ReactNode, useContext } from "react";
import { useAuth as useOidcAuth } from "react-oidc-context";

// Definizione dell'interfaccia per il profilo utente di Keycloak
interface KeycloakProfile extends UserProfile {
  realm_access?: {
    roles: string[];
  };
  resource_access?: {
    [clientId: string]: {
      roles: string[];
    };
  };
  preferred_username?: string;
  email?: string;
  given_name?: string;
  family_name?: string;
}

// Estensione dell'interfaccia User per includere il profilo Keycloak
interface KeycloakUser extends User {
  profile: KeycloakProfile;
}

interface AuthContextType {
  isAuthenticated: boolean;
  isLoading: boolean;
  hasRole: (role: string) => boolean;
  hasAnyRole: (roles: string[]) => boolean;
  hasAllRoles: (roles: string[]) => boolean;
  login: () => void;
  logout: () => void;
  getAccessToken: () => string | undefined;
  user: KeycloakUser | null;
}

const AuthContext = createContext<AuthContextType | undefined>(undefined);

export function CustomAuthProvider({ children }: { children: ReactNode }) {
  const auth = useOidcAuth();

  // Funzione per verificare se l'utente ha un ruolo specifico
  const hasRole = (role: string): boolean => {
    if (!auth.isAuthenticated || !auth.user) return false;
    
    // Utilizziamo il tipo specifico per Keycloak
    const keycloakUser = auth.user as KeycloakUser;
    const userRoles = keycloakUser.profile.realm_access?.roles || [];
    return userRoles.includes(role);
  };

  // Funzione per verificare se l'utente ha almeno uno dei ruoli
  const hasAnyRole = (roles: string[]): boolean => {
    return roles.some(role => hasRole(role));
  };

  // Funzione per verificare se l'utente ha tutti i ruoli
  const hasAllRoles = (roles: string[]): boolean => {
    return roles.every(role => hasRole(role));
  };

  // Funzione per ottenere il token di accesso
  const getAccessToken = (): string | undefined => {
    return auth.user?.access_token;
  };

  const value: AuthContextType = {
    isAuthenticated: auth.isAuthenticated,
    isLoading: auth.isLoading,
    hasRole,
    hasAnyRole,
    hasAllRoles,
    login: () => void auth.signinRedirect(),
    logout: () => void auth.removeUser(),
    getAccessToken,
    user: auth.user as KeycloakUser | null
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export const useAuth = (): AuthContextType => {
  const context = useContext(AuthContext);
  if (context === undefined) {
    throw new Error("useAuth must be used within a CustomAuthProvider");
  }
  return context;
};
