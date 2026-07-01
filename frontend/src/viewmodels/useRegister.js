import { useState } from 'react';
import api from '../services/api';

export const useRegister = () => {
    const [isLoading, setIsLoading] = useState(false);
    const [error, setError] = useState(null);

    const register = async (type, userData) => {
        setIsLoading(true);
        setError(null);
        try {
            let payload = userData;

            if (type === 'Agent') {
                payload = {
                    fullName: `${userData.firstName} ${userData.lastName}`,
                    email: userData.email,
                    password: userData.password,
                    employeeId: userData.employeeId,
                };
            } 
            else {
                payload = {
                    fullName: `${userData.firstName} ${userData.lastName}`,
                    email: userData.email,
                    password: userData.password,
                    socialInsuranceNumber: userData.socialInsuranceNumber,
                };
            }
            const endpoint = type === 'Agent' ? '/Agent/create' : '/user';
            
            await api.request(endpoint, {
                method: 'POST',
                body: JSON.stringify(payload),
            });
            return { success: true };
        } catch (err) {
            setError(err.message);
            throw err;
        } finally {
            setIsLoading(false);
        }
    };
    return { register, isLoading, error };
};