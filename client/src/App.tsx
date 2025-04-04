// App.tsx
import { BrowserRouter } from "react-router-dom"
import { ShopRoutes } from './Routes'
import { CustomAuthProvider } from './shared/components/auth/AuthContext'
import { NavBar } from './shared/components/core/NavBar'

function App() {
  return (
    <>
      <BrowserRouter>
        <CustomAuthProvider>
          <header>
            <NavBar></NavBar>
          </header>
          <div className="page">
            <ShopRoutes></ShopRoutes>
          </div>
        </CustomAuthProvider>
      </BrowserRouter>
    </>
  )
}

export default App