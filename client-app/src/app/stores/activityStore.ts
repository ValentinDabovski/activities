import { makeAutoObservable } from "mobx";
import { Activity } from "../models/activity";
import agent from "../api/agent";

export default class ActivityStore {

    activities: Activity[] = []
    selectedActivity: Activity | null = null
    editMode = false
    loading = false
    loadinInitial = false

    constructor() {
        makeAutoObservable(this)
    }

    loadActivities = async () => {
        this.setLoadingInitial(true)
        try {
            const activities = await agent.Acitvities.list();
            activities.forEach(a => {
                a.date = a.date.split('T')[0]
                this.activities.push(a)
            })
            this.setLoadingInitial(false)

        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false)
        }
    }

    setLoadingInitial = (state: boolean) => {
        this.loadinInitial = state
    }
} 