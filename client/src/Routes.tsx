import { Navigate, useRoutes } from "react-router-dom";
import { HomePage, ProfilePage } from "./pages";
import { ProtectedRoute } from "./shared/components/auth/ProtectedRoute";

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
            element: <ProtectedRoute><ProfilePage></ProfilePage></ProtectedRoute>
        },
        {
            path: '*',
            element: <Navigate to="/"></Navigate>
        },
    ])
}