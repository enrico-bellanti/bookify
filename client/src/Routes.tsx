import { Navigate, useRoutes } from "react-router-dom";
import HomePage from "./pages/home/HomePage";

export function ShopRoutes(){
    return  useRoutes([
        {
            path: '/',
            element: <HomePage></HomePage>
        },
        {
            path: '/',
            element: <Navigate to="shop"></Navigate>
        },
        {
            path: '*',
            element: <Navigate to="/"></Navigate>
        },
    ])
}