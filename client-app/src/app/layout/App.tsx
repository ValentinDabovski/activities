import { useEffect, useState } from 'react';
import './styles.css';
import { Container } from 'semantic-ui-react';
import { Activity } from '../models/activity';
import NavBar from './NavBar';
import ActivityDashboard from '../../features/activities/dashboard/ActivityDashboard';
import { v4 as uuid } from 'uuid'
import agent from '../api/agent';
import LoadingComponent from './LoadingComponent';

function App() {

  const [activities, setActivities] = useState<Activity[]>([])
  const [selectedActivity, setSelectedActivity] = useState<Activity | undefined>(undefined)
  const [editMode, setEditMode] = useState(false)
  const [loading, setLoading] = useState(true)
  const [submitting, setSubmitting] = useState(false)

  useEffect(() => {
    agent.Acitvities.list().then(res => {
      let activities: Activity[] = [];
      res.forEach(a => {
        a.date = a.date.split('T')[0]
        activities.push(a)
      })
      setActivities(activities)
      setLoading(false)
    })
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
    setSubmitting(true)

    if (activity.id) {
      agent.Acitvities.update(activity).then(res => {
        setActivities([...activities.filter(x => x.id !== activity.id), activity])
        setSelectedActivity(activity)
        setEditMode(false)
        setSubmitting(false)
      })
    } else {
      activity.id = uuid()
      agent.Acitvities.create(activity)
      setActivities([...activities, activity])
      setSelectedActivity(activity)
      setEditMode(false)
      setSubmitting(false)
    }
  }

  function handleDeleteActivity(id: string) {
    setSubmitting(true)
    agent.Acitvities.delete(id).then(() => {
      setActivities([...activities.filter(x => x.id !== id)])
      setSubmitting(false)
    })
  }

  if (loading) return <LoadingComponent content={'Loading app'} />

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
          deleteActivity={handleDeleteActivity}
          submitting={submitting}
        />
      </Container>
    </>
  );
}

export default App;
