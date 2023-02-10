import { RouteObject, createBrowserRouter } from "react-router-dom";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetails from "../../features/activities/details/ActivityDetails";

export const routes: RouteObject[] = [
    {
        path: '',
        element: <HomePage />
    },
    {
        path: '/',
        element: <App />,
        children: [
            {
                path: '/activities', element: <App />, children: [
                    { path: 'all', element: <ActivityDashboard /> },
                    { path: 'create', element: <ActivityForm /> },
                    { path: 'details/:id', element: <ActivityDetails key='details' /> },
                    { path: 'edit/:id', element: <ActivityForm key='edit' /> }
                ]
            }
        ],
    }
]

export const router = createBrowserRouter(routes)