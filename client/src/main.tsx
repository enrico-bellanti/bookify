import { createRoot } from 'react-dom/client'
import { AuthProvider } from 'react-oidc-context'
import App from './App.tsx'
import './index.css'
import { oidcConfig } from './shared/components/auth/auth.config.ts'

createRoot(document.getElementById('root')!).render(
  <AuthProvider {...oidcConfig}>
    <App />
  </AuthProvider>
  // <StrictMode>
  // </StrictMode>,
)
