import React from 'react'
import { Button, Container, Menu } from 'semantic-ui-react'
import { observer } from 'mobx-react-lite'
import { NavLink } from 'react-router-dom'

export default observer(function NavBar() {
    return (
        <Menu inverted fixed='top'>
            <Container>
                <Menu.Item as={NavLink} to='/' header>
                    <img src='/assets/logo.png' alt='logo' style={{ marginRight: '10px' }} /> Home
                </Menu.Item>
                <Menu.Item as={NavLink} to='/activities/all' name='Activities' />
                <Menu.Item>
                    <Button as={NavLink} to='/activities/create' positive content='Create activity' />
                </Menu.Item>
            </Container>
        </Menu>
    )
})