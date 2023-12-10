import axios, { AxiosResponse } from "axios";
import { Activity } from "../models/activity";
import  {userManager}  from "../identity/userManagerStore";


axios.defaults.baseURL = 'https://localhost:5001/api'

userManager.getUser().then(user => {
    if(user) {
        axios.defaults.headers.common['Authorization'] = `Bearer ${user.access_token}`
    }
})
const responseBody = <T>(response: AxiosResponse<T>) => response.data;

const requests = {
    get: <T>(url: string) => axios.get<T>(url).then(responseBody),
    post: <T>(url: string, body: {}) => axios.post<T>(url, body).then(responseBody),
    put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
    delete: <T>(url: string) => axios.delete<T>(url).then(responseBody)
}

const activitiesApiUrl = {
    list: '/activities',
    details: (id: string) => `/activities/${id}`,
    create: '/activities',
    update: (id: string) => `activities/${id}`,
    delete: (id: string) => `activities/${id}`
}

const Acitvities = {
    list: () => requests.get<Activity[]>(activitiesApiUrl.list),
    details: (id: string) => requests.get<Activity>(activitiesApiUrl.details(id)),
    create: (activity: Activity) => requests.post<void>(activitiesApiUrl.create, activity),
    update: (activity: Activity) => requests.put<void>(activitiesApiUrl.update(activity.id), activity),
    delete: (id: string) => requests.delete<void>(activitiesApiUrl.delete(id))
}

const agent = {
    Acitvities
}

export default agent