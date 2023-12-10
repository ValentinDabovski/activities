import { makeAutoObservable, runInAction } from "mobx";
import { Activity } from "../models/activity";
import agent from "../api/agent";

export default class UserStore {
    
    isUserLoggedIn = false

    constructor() {
        makeAutoObservable(this)
    }

    setIsUserLoggedIn = (state: boolean) => {
        this.isUserLoggedIn = state
    }
} 