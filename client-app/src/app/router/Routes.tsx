import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";

export const routes: RouteObject[] = [
    {
        path: '/',
        element: <App />,
        children: [
            { path: '', element: <HomePage /> },
            {
                path: '/activities', element: <App />, children: [
                    { path: 'all', element: <ActivityDashboard /> },
                    { path: 'create', element: <ActivityForm /> },
                    { path: 'details/:id', element: <ActivityDetails /> }
                ]
            }
        ],
    }
]

export const router = createBrowserRouter(routes)