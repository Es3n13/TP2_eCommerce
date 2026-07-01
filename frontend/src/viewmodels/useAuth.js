import { useState } from 'react';
import api from '../services/api';

export const useAuth = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const login = async (email, password) => {
        setIsLoading(true);
        setError(null);
        try {
            const data = await api.request('/auth/login', {
                method: 'POST',
                body: JSON.stringify({ email, password }),
            });

            // On stocke le token et le rôle dans le localStorage
            localStorage.setItem('token', data.token);
            localStorage.setItem('role', data.role);

            return data; // Retourne { token, role }
        } catch (err) {
            setError(err.message);
            throw err;
        } finally {
            setIsLoading(false);
        }
    };

    const logout = () => {
        localStorage.removeItem('token');
        localStorage.removeItem('role');
        window.location.href = '/login';
    };

    return { login, logout, isLoading, error };
};