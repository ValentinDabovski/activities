import React, { SyntheticEvent, useState } from "react";
import { Activity } from "../../../app/models/activity";
import { Button, Item, Label, Segment } from "semantic-ui-react";

interface Props {
    activities: Activity[]
    selectActivity: (id: string) => void
    deleteActivity: (id: string) => void
    submitting: boolean
}

export default function ActivityList({ activities, selectActivity, deleteActivity, submitting }: Props) {

    const [target, setTarget] = useState('')

    function handleAcitivityDelete(e: SyntheticEvent<HTMLButtonElement>, id: string) {
        setTarget(e.currentTarget.name)
        deleteActivity(id)
    }

    return (
        <Segment>
            <Item.Group divided>
                {activities.map(activity => (
                    <Item key={activity.id}>
                        <Item.Content>
                            <Item.Header as='a'>{activity.title}</Item.Header>
                            <Item.Meta>{activity.date.toString()}</Item.Meta>
                            <Item.Description>
                                <div>{activity.description}</div>
                                <div>{activity.address.city}, {activity.address.venue}</div>
                            </Item.Description>
                            <Item.Extra>
                                <Button onClick={() => selectActivity(activity.id)} floated="right" basic color='grey' content="View" />
                                <Button
                                    name={activity.id}
                                    loading={submitting && target === activity.id}
                                    onClick={(e) => handleAcitivityDelete(e, activity.id)}
                                    floated='right'
                                    basic color="red"
                                    content='Delete'
                                />


                                <Label basic content={activity.category.name} />
                            </Item.Extra>
                        </Item.Content>
                    </Item>
                ))}
            </Item.Group>
        </Segment>
    )
}