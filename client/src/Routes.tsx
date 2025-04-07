import { Navigate, useRoutes } from "react-router-dom";
import HomePage from "./pages/home/HomePage";
import Profile from "./pages/profile/Profile";

export function ShopRoutes(){
    return  useRoutes([
        {
            path: '/',
            element: <HomePage></HomePage>
        },
        // {
        //     path: '/',
        //     element: <Navigate to="shop"></Navigate>
        // },
        {
            path: '/profile',
            element: <Profile></Profile>
        },
        {
            path: '*',
            element: <Navigate to="/"></Navigate>
        },
    ])
}