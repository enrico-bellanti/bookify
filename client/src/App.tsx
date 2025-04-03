import './App.css'
import { HomePage } from './pages/home/HomePage'
import { NavBar } from './shared/components/core/NavBar'

function App() {

  return (
    <>
      <header>
        <NavBar></NavBar>
      </header>
      <div className='page'>
        <HomePage></HomePage>
      </div>
    </>
  )
}

export default App
