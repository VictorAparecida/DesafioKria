import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Card, Container, Row, Col } from 'react-bootstrap';

const Home = () => {
    const [repositories, setRepositories] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:5001/api/meus-repositorios')
            .then(response => setRepositories(response.data));
    }, []);

    return (
        <Container>
            <Row xs={1} md={2} lg={3} xl={4} className="mt-4">
                {repositories.map(repository => (
                    <Col key={repository.id} className="mb-4">
                        <Card>
                            <Card.Body>
                                <Card.Title>{repository.name}</Card.Title>
                                <Card.Text>{repository.description}</Card.Text>
                                <div className="d-flex justify-content-between align-items-center">
                                    <div>
                                        <Card.Subtitle className="mb-2 text-muted">Linguagem: {repository.language}</Card.Subtitle>
                                        <Card.Subtitle className="mb-2 text-muted">Última atualização: {repository.updated_at}</Card.Subtitle>
                                    </div>
                                    <div>
                                        <Card.Subtitle className="mb-2 text-muted">Dono: {repository.owner.login}</Card.Subtitle>
                                    </div>
                                </div>
                                <Button href={`/repository/${repository.owner.login}/${repository.name}`} variant="primary">Detalhes</Button>
                            </Card.Body>
                        </Card>
                    </Col>
                ))}
            </Row>
        </Container>
    );
};

export default Home;
