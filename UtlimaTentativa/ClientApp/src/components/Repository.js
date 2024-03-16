import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { Button, Card, ListGroup } from 'react-bootstrap';

const Repository = ({ match }) => {
    const [repository, setRepository] = useState(null);

    useEffect(() => {
        axios.get(`https://localhost:5001/api/repositories/${match.params.owner}/${match.params.name}`)
          .then(response => setRepository(response.data));
    }, );

    const handleFavorite = () => {
        axios.post(`https://localhost:5001/api/repositories/${match.params.owner}/${match.params.name}/favoritar`);
    };

    if (!repository) {
        return <p>Carregando...</p>;
    }

    return (
        <Card>
            <Card.Body>
                <Card.Title>{repository.name}</Card.Title>
                <Card.Text>{repository.description}</Card.Text>
                <Card.Subtitle>Linguagem: {repository.language}</Card.Subtitle>
                <Card.Subtitle>Última atualização: {repository.updated_at}</Card.Subtitle>
                <Card.Subtitle>Dono: {repository.owner.login}</Card.Subtitle>
                <Button onClick={handleFavorite}>Favoritar</Button>
            </Card.Body>
            <ListGroup variant="flush">
                <ListGroup.Item>
                    <h5>Contribuidores:</h5>
                    <ul>
                        {repository.contributors.map(contributor => (
                            <li key={contributor.id}>{contributor.login}</li>
                        ))}
                    </ul>
                </ListGroup.Item>
            </ListGroup>
        </Card>
    );
};

export default Repository;
