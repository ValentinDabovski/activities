import './styles.css';
import { Container } from 'semantic-ui-react';
import NavBar from './NavBar';
import { observer } from 'mobx-react-lite';
import { Outlet } from 'react-router-dom';
import { useEffect } from 'react';
import { useAuth } from 'react-oidc-context';
function App() {

    const auth = useAuth();
    
    useEffect(() => {
        auth.signinRedirect()
    }, [auth]);
    
  return (
    <>
      <NavBar />
      <Container style={{ marginTop: '7em' }}>
        <Outlet />
      </Container>
    </>
  );
}

export default observer(App);
