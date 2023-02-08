import { makeAutoObservable, runInAction } from "mobx";
import { Activity } from "../models/activity";
import agent from "../api/agent";

export default class ActivityStore {

    activities: Activity[] = []
    selectedActivity: Activity | undefined = undefined
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

    selectActivity = (id: string) => {
        this.selectedActivity = this.activities.find(a => a.id === id)
    }

    cancelSelectedActivity = () => {
        this.selectedActivity = undefined
    }

    openForm = (id?: string) => {
        id ? this.selectActivity(id) : this.cancelSelectedActivity()
        this.editMode = true
    }

    closeForm = () => {
        this.editMode = false
    }

    createActivity = async (activity: Activity) => {
        this.loading = true

        try {
            await agent.Acitvities.create(activity)
            runInAction(() => {
                this.activities.push(activity)
                this.selectedActivity = activity
                this.editMode = false
                this.loading = false
            })
        } catch (error) {
            console.log(error)
            runInAction(() => {
                this.loading = false
            })
        }
    }

    editActivity = async (activity: Activity) => {
        this.loading = true

        try {
            await agent.Acitvities.update(activity);
            runInAction(() => {
                this.activities = [...this.activities.filter(a => a.id !== activity.id), activity]
                this.selectedActivity = activity
                this.editMode = false
                this.loading = false
            })
        } catch (error) { 
            console.log(error)
            runInAction(() => {
                this.loading = false
            })
        }
    }
} 