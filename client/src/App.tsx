// App.tsx
import { CustomAuthProvider, NavBar } from "@/shared/";
import { BrowserRouter } from "react-router-dom";
import { ShopRoutes } from './Routes';


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