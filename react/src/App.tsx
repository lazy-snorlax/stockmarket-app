import { Outlet } from 'react-router-dom'
import Navbar from './components/Navbar'
import { UserProvider } from './context/useAuth'
import { ToastContainer } from 'react-toastify'

function App() {
  return (
    <>
      <UserProvider>
        <Navbar />
        <Outlet />
        <ToastContainer />
      </UserProvider>
    </>
  )
}

export default App
