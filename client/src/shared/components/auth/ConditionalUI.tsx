// src/auth/ConditionalUI.tsx
import { ReactNode } from "react";
import { useAuth } from "./AuthContext";

interface ShowToAuthenticatedProps {
  children: ReactNode;
  fallback?: ReactNode;
}

export function ShowToAuthenticated({
  children,
  fallback = null,
}: ShowToAuthenticatedProps) {
  const auth = useAuth();
  return <>{auth.isAuthenticated ? children : fallback}</>;
}

interface ShowToUnauthenticatedProps {
  children: ReactNode;
  fallback?: ReactNode;
}

export function ShowToUnauthenticated({
  children,
  fallback = null,
}: ShowToUnauthenticatedProps) {
  const auth = useAuth();
  return <>{!auth.isAuthenticated ? children : fallback}</>;
}

interface ShowToRolesProps {
  roles: string[];
  requireAll?: boolean;
  children: ReactNode;
  fallback?: ReactNode;
}

export function ShowToRoles({
  roles,
  requireAll = false,
  children,
  fallback = null,
}: ShowToRolesProps) {
  const auth = useAuth();
  const hasAccess = requireAll
    ? auth.hasAllRoles(roles)
    : auth.hasAnyRole(roles);

  return <>{hasAccess ? children : fallback}</>;
}