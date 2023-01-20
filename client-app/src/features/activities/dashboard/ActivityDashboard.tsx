import React from "react";
import { Grid, GridColumn, List } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";
import ActivityList from "./ActivityList";
import ActivityDetails from "../details/ActivityDetails";
import ActivityForm from "../form/ActivityForm";

interface Props {
    activities: Activity[]
    selectedActivity: Activity | undefined
    selectActivity: (id: string) => void
    cancelSelectActivity: () => void
    editMode: boolean
    closeForm: () => void
    openForm: (id: string) => void
    createOrEdit: (activity: Activity) => void
}

export default function ActivityDashboard(
    { activities,
        selectActivity,
        selectedActivity,
        cancelSelectActivity,
        editMode,
        closeForm,
        openForm,
        createOrEdit }: Props) {
    return (

        <Grid>
            <Grid.Column width='10'>
                <ActivityList activities={activities} selectActivity={selectActivity} />
            </Grid.Column>
            <Grid.Column width='6'>
                {selectedActivity && !editMode &&
                    <ActivityDetails
                        activity={selectedActivity}
                        cancelSelectActivity={cancelSelectActivity}
                        openForm={openForm}
                    />}
                {editMode &&
                    <ActivityForm 
                        closeForm={closeForm} activity={selectedActivity} 
                        createOrEdit={createOrEdit}
                    />}
            </Grid.Column>
        </Grid>

    )
}