import React, { createContext, useContext, useState, useEffect } from 'react';

interface AuthContextType {
    isAuthenticated: boolean;
    username: string;
    login: (authData: { id: string; name: string; token: string; }) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType>({
    isAuthenticated: false,
    username: '',
    login: () => {},
    logout: () => {},
});

export const AuthProvider: React.FC<{ children: React.ReactNode }> = ({ children }) => {
    const [isAuthenticated, setIsAuthenticated] = useState(false);
    const [username, setUsername] = useState('');

    useEffect(() => {
        const name = localStorage.getItem('userName');
        if (name) {
            setUsername(name);
            setIsAuthenticated(true);
        }
    }, []);

    const login = (authData: { id: string; name: string; token: string; }) => {
        localStorage.setItem('authToken', authData.token);
        localStorage.setItem('userId', authData.id);
        localStorage.setItem('userName', authData.name);

        setUsername(authData.name);
        setIsAuthenticated(true);
    };


    const logout = () => {
        localStorage.removeItem('authToken');
        localStorage.removeItem('userId');
        localStorage.removeItem('userName');

        setUsername('');
        setIsAuthenticated(false);
    };

    return (
        <AuthContext.Provider value={{ isAuthenticated, username, login, logout }}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);
