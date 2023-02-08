import { makeAutoObservable, runInAction } from "mobx";
import { Activity } from "../models/activity";
import agent from "../api/agent";

export default class ActivityStore {

    activityRegistry = new Map<string, Activity>()
    selectedActivity: Activity | undefined = undefined
    editMode = false
    loading = false
    loadinInitial = true

    constructor() {
        makeAutoObservable(this)
    }

    get activitiesByDate() {
        return Array
            .from(this.activityRegistry.values())
            .sort((a, b) => Date.parse(a.date) - Date.parse(b.date))
    }

    loadActivities = async () => {
        try {
            const activities = await agent.Acitvities.list();
            activities.forEach(a => {
                a.date = a.date.split('T')[0]
                this.activityRegistry.set(a.id, a)
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
        this.selectedActivity = this.activityRegistry.get(id)
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
                this.activityRegistry.set(activity.id, activity)
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
                this.activityRegistry.set(activity.id, activity)
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

    deleteActivity = async (id: string) => {
        try {
            await agent.Acitvities.delete(id)
            runInAction(() => {
                this.activityRegistry.delete(id)
                if (this.selectedActivity?.id === id) this.cancelSelectedActivity()
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