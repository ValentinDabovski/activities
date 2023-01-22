import React, { ChangeEvent, useState } from "react";
import { Button, Form, Segment } from "semantic-ui-react";
import { Activity } from "../../../app/models/activity";

interface Props {
    activity: Activity | undefined
    closeForm: () => void
    createOrEdit: (activity: Activity) => void
    submitting: boolean
}

export default function ActivityForm({ activity: selectedActivity, closeForm, createOrEdit, submitting }: Props) {

    const initialState = selectedActivity ?? {
        id: '',
        title: '',
        category: '',
        description: '',
        date: '',
        city: '',
        venue: ''
    }

    const [activity, setActivity] = useState(initialState)

    function handleSubmit() {
        createOrEdit(activity)
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target
        setActivity({ ...activity, [name]: value })
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input placeholder='Title' onChange={handleInputChange} value={activity.title} name='title' />
                <Form.TextArea placeholder='Description' onChange={handleInputChange} value={activity.description} name='description' />
                <Form.Input placeholder='Category' onChange={handleInputChange} value={activity.category} name='category' />
                <Form.Input type="date" placeholder='Date' onChange={handleInputChange} value={activity.date} name='date' />
                <Form.Input placeholder='City' onChange={handleInputChange} value={activity.city} name='city' />
                <Form.Input placeholder='Venue' onChange={handleInputChange} value={activity.venue} name='venue' />
                <Button loading={submitting} basic floated="right" positive type="submit" content='Submit' />
                <Button onClick={() => closeForm()} basic floated="right" type="button" content='Cancel ' />
            </Form>
        </Segment>
    )
}