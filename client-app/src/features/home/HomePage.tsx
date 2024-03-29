import { observer } from "mobx-react-lite";
import { Link } from "react-router-dom";
import { Container, Header, Segment, Image, Button } from "semantic-ui-react";

function HomePage() {
    return (
        <Segment inverted textAlign="center" vertical className="masthead">
            <Container text>
                <Header as='h1' inverted>
                    <Image size='massive' src='/assets/logo.png' alt='logo' style={{ marginBottom: 12 }} />
                    Activities
                </Header>
                <Header as='h2' inverted content='Welcome' />
                <Button as={Link} to='/activities/all' size="huge" content='Take me to the Activities!' inverted  />
            </Container>
        </Segment>
    )
}

export default observer(HomePage);