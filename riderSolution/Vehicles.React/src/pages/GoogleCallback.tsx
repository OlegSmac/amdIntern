import { useEffect, useRef } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';

const GoogleCallback = () => {
    const { login } = useAuth();
    const navigate = useNavigate();
    const called = useRef(false);

    useEffect(() => {
        if (called.current) return;
        called.current = true;

        const params = new URLSearchParams(window.location.search);

        const token = params.get('token');
        const id = params.get('id');
        const name = params.get('name');

        if (id && name && token) {
            login({ id, name, token }); 
            navigate('/');
        } else {
            navigate('/login');
        }
    }, []);

    return <p>Logging you in with Google...</p>;
};

export default GoogleCallback;
