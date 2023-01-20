import React, { useEffect, useState } from 'react';
import './styles.css';
import axios from 'axios';
import { Container, Header, List } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';

function App() {

  const [activities, setActivities] = useState<Activity[]>([])
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined)
  const [editMode, setEditMode] = useState(false)

  useEffect(() => {
    axios.get('http://localhost:5000/api/activities')
      .then(res => { setActivities(res.data) })
  }, [])


  function handleSelectActivity(id: string) {
    setSelectedActivity(activities.find(x => x.id === id))
  }

  function handleCancelSelectActivity() {
    setSelectedActivity(undefined)
  }

  function handleFormOpen(id?: string) {
    id ? handleSelectActivity(id) : handleCancelSelectActivity()
    setEditMode(true)
  }

  function handleFromClose() {
    setEditMode(false)
  }

  function handleCreateOrEditActivity(activity: Activity) {
    activity.id
      ? setActivities([...activities.filter(x => x.id !== activity.id), activity])
      : setActivities([...activities, activity])
    setEditMode(false)
    setSelectedActivity(activity)
  }

  return (
    <>
      <NavBar openForm={handleFormOpen} />
      <Container style={{ marginTop: '7em' }}>
        <ActivityDashboard
          selectedActivity={selectedActivity}
          selectActivity={handleSelectActivity}
          cancelSelectActivity={handleCancelSelectActivity}
          activities={activities}
          editMode={editMode}
          openForm={handleFormOpen}
          closeForm={handleFromClose}
          createOrEdit={handleCreateOrEditActivity}
        />
      </Container>
    </>
  );
}

export default App;
