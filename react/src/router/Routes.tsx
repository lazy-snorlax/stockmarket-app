import { createBrowserRouter } from 'react-router-dom'
import App from '../App'
import LoginPage from '../views/Login'
import HomePage from '../views/Home'


export const router = createBrowserRouter([
    {
        path: "/",
        element: <App />,
        children: [
            { path: "", element: <HomePage /> },
            { path: "login", element: <LoginPage /> },
        ]
    }
])