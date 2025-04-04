// src/auth/ProtectedRoute.tsx
import React from "react";
import { Navigate, useLocation } from "react-router-dom";
import { useAuth } from "./AuthContext";

interface ProtectedRouteProps {
  children: React.ReactNode;
  requiredRoles?: string[];
  requireAllRoles?: boolean;
}

export function ProtectedRoute({
  children,
  requiredRoles = [],
  requireAllRoles = false
}: ProtectedRouteProps) {
  const auth = useAuth();
  const location = useLocation();

  if (auth.isLoading) {
    return <div>Caricamento...</div>;
  }

  // Verifica l'autenticazione
  if (!auth.isAuthenticated) {
    return <Navigate to="/login" state={{ from: location }} replace />;
  }

  // Se non ci sono ruoli richiesti, mostra il contenuto
  if (requiredRoles.length === 0) {
    return <>{children}</>;
  }

  // Verifica i ruoli
  const hasRequiredRoles = requireAllRoles
    ? auth.hasAllRoles(requiredRoles)
    : auth.hasAnyRole(requiredRoles);

  if (!hasRequiredRoles) {
    return <Navigate to="/unauthorized" replace />;
  }

  return <>{children}</>;
}