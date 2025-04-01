import './App.css'
import { Home } from './pages/home/Home'
import { NavBar } from './shared/components/core/NavBar'

function App() {

  return (
    <>
      <header>
        <NavBar></NavBar>
      </header>
      <div className='page'>
        <Home></Home>
      </div>
    </>
  )
}

export default App
