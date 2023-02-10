import { makeAutoObservable, runInAction } from "mobx";
import { Activity } from "../models/activity";
import agent from "../api/agent";

export default class ActivityStore {

    activityRegistry = new Map<string, Activity>()
    selectedActivity: Activity | undefined = undefined
    editMode = false
    loading = false
    loadinInitial = false

    constructor() {
        makeAutoObservable(this)
    }

    get activitiesByDate() {
        return Array
            .from(this.activityRegistry.values())
            .sort((a, b) => Date.parse(a.date) - Date.parse(b.date))
    }

    loadActivities = async () => {
        this.setLoadingInitial(true)
        try {
            const activities = await agent.Acitvities.list();
            activities.forEach(a => {
                this.setActivity(a)
            })
            this.setLoadingInitial(false)

        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false)
        }
    }

    loadActivity = async (id: string) => {
        let acitvity = this.getActivity(id)
        if (acitvity) {
            this.selectedActivity = acitvity
            return acitvity
        }
        else {
            this.setLoadingInitial(true)
            try {
                acitvity = await agent.Acitvities.details(id)
                this.setActivity(acitvity)
                runInAction(() => this.selectedActivity = acitvity)
                this.setLoadingInitial(false)
                return acitvity
            } catch (error) {
                console.log(error)
                this.setLoadingInitial(false)
            }
        }

    }

    private setActivity = (activity: Activity) => {
        activity.date = activity.date.split('T')[0]
        this.activityRegistry.set(activity.id, activity)
    }

    private getActivity = (id: string) => {
        return this.activityRegistry.get(id)
    }

    setLoadingInitial = (state: boolean) => {
        this.loadinInitial = state
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
            this.setLoadingInitial(false)
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
            this.setLoadingInitial(false)
        }
    }

    deleteActivity = async (id: string) => {
        this.loading = true
        try {
            await agent.Acitvities.delete(id)
            runInAction(() => {
                this.activityRegistry.delete(id)
                this.loading = false
            })
        } catch (error) {
            console.log(error)
            this.setLoadingInitial(false)
        }
    }
} 