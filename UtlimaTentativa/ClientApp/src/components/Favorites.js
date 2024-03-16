import React, { useState, useEffect } from 'react';
import axios from 'axios';
import {  Card } from 'react-bootstrap';

const Favorites = () => {
    const [favorites, setFavorites] = useState([]);

    useEffect(() => {
        axios.get('https://localhost:5001/api/favorites')
            .then(response => setFavorites(response.data));
    }, []);

    return (
        <div>
            {favorites.map(favorite => (
                <Card key={`${favorite.owner}-${favorite.name}`}>
                    <Card.Body>
                        <Card.Title>{favorite.name}</Card.Title>
                        <Card.Text>{favorite.description}</Card.Text>
                        <Card.Subtitle>Dono: {favorite.owner}</Card.Subtitle>
                    </Card.Body>
                </Card>
            ))}
        </div>
    );
};

export default Favorites;