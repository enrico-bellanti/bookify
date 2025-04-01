import './App.css'
import Test from './pages/test/Test'
import { NavBar } from './shared/components/core/NavBar'

function App() {

  return (
    <>
      <header>
        <NavBar></NavBar>
      </header>
      <div className='page'>
        <Test></Test>
      </div>
    </>
  )
}

export default App
