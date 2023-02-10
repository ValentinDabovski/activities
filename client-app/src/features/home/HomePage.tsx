import { Link } from "react-router-dom";
import { Container } from "semantic-ui-react";

export default function HomePage() {
    return (
        <Container stype={{ marginTop: '7em' }}>
            <h1>Home Page</h1>
            <h3>
                Go to <Link to='/activities/all'>Activities</Link>
            </h3>
        </Container>
    )
}