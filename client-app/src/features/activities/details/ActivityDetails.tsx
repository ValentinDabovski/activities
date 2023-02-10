import React, { useEffect } from "react";
import { Button, Card, Image } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import LoadingComponent from "../../../app/layout/LoadingComponent";
import { observer } from "mobx-react-lite";
import { useParams } from "react-router-dom";

export default observer(function ActivityDetails() {

    const { activityStore } = useStore()
    const { selectedActivity: activity, loadActivity, loadinInitial } = activityStore
    const { id } = useParams()

    useEffect(() => {
        if (id) loadActivity(id)
    }, [id, loadActivity]) 

    if (loadinInitial || !activity) return <LoadingComponent />
    return (
        <Card fluid>
            <Image src={`/assets/categoryImages/${activity.category.name}.jpg`} />
            <Card.Content>
                <Card.Header>{activity.title}</Card.Header>
                <Card.Meta>
                    <span>{activity.date.toString()}</span>
                </Card.Meta>
                <Card.Description>
                    {activity.description}
                </Card.Description>
            </Card.Content>
            <Card.Content extra>
                <Button.Group>
                    <Button basic content='Edit' />
                    <Button basic content='Cancel' />
                </Button.Group>
            </Card.Content>
        </Card>
    )
})