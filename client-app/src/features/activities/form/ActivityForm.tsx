import React, { ChangeEvent, useState } from "react";
import { Button, Form, Segment } from "semantic-ui-react";
import { useStore } from "../../../app/stores/store";
import { observer } from "mobx-react-lite";


export default observer(function ActivityForm() {

    const { activityStore } = useStore()
    const { selectedActivity, loading, createActivity, editActivity } = activityStore

    const initialState = selectedActivity ?? {
        id: '',
        title: '',
        date: '',
        description: '',
        category: {
            name: '',
            description: ''
        },
        address: {
            street: '',
            city: '',
            state: '',
            country: '',
            zipCode: '',
            venue: ''
        }
    }

    const [activity, setActivity] = useState(initialState)

    function handleSubmit() {
        activity.id ? editActivity(activity) : createActivity(activity)
    }

    function handleInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target
        setActivity({ ...activity, [name]: value })
    }

    function handAddressInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target
        setActivity({ ...activity, address: { ...activity.address, [name]: value } })
    }

    function handCategoryInputChange(event: ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) {
        const { name, value } = event.target
        setActivity({ ...activity, category: { ...activity.category, [name]: value } })
    }

    return (
        <Segment clearing>
            <Form onSubmit={handleSubmit} autoComplete='off'>
                <Form.Input placeholder='Title' onChange={handleInputChange} value={activity.title} name='title' />
                <Form.TextArea placeholder='Description' onChange={handleInputChange} value={activity.description} name='description' />
                <Form.Input placeholder='Category' onChange={handCategoryInputChange} value={activity.category.name} name='name' />
                <Form.Input type="date" placeholder='Date' onChange={handleInputChange} value={activity.date} name='date' />
                <Form.Input placeholder='City' onChange={handAddressInputChange} value={activity.address.city} name='city' />
                <Form.Input placeholder='Venue' onChange={handAddressInputChange} value={activity.address.venue} name='venue' />
                <Button loading={loading} basic floated="right" positive type="submit" content='Submit' />
                <Button basic floated="right" type="button" content='Cancel ' />
            </Form>
        </Segment>
    )
}) 