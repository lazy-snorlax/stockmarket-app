import { createBrowserRouter } from 'react-router-dom'
import App from '../App'
import LoginPage from '../views/Login'
import HomePage from '../views/Home'
import RegisterPage from '../views/Register'
import SearchPage from '../views/Search'


export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            { path: "", element: <HomePage /> },
            { path: "login", element: <LoginPage /> },
            { path: "register", element: <RegisterPage /> },
            { path: "search", element: <SearchPage />}
        ]
    }
])