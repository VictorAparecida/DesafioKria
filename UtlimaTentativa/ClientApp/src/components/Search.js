import React, { useState } from 'react';
import { Button, Form } from 'react-bootstrap';

const Search = ({ history }) => {
    const [query, setQuery] = useState('');

    const handleSubmit = (event) => {
        event.preventDefault();
        history.push(`/search/${query}`);
    };

    return (
        <div className="container">
            <div className="row justify-content-center mt-4">
                <div className="col-md-6">
                    <Form onSubmit={handleSubmit}>
                        <Form.Group controlId="formBasicSearch">
                            <Form.Label>Nome do repositório</Form.Label>
                            <Form.Control
                                type="text"
                                placeholder="Digite o nome do repositório"
                                value={query}
                                onChange={event => setQuery(event.target.value)}
                            />
                        </Form.Group>
                        <Button variant="primary" type="submit" block>
                            Procurar
                        </Button>
                    </Form>
                </div>
            </div>
        </div>
    );
};

export default Search;
