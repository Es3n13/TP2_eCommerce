const API_BASE_URL = 'http://localhost:5106/api';

const api = {
    async request(endpoint, options = {}) {
        const token = localStorage.getItem('token');
        
        const headers = {};

        if (!(options.body instanceof FormData)) {
            headers['Content-Type'] = 'application/json';
        }

        if (token) {
            headers['Authorization'] = `Bearer ${token}`;
        }

        const response = await fetch(`${API_BASE_URL}${endpoint}`, {
            ...options,
            headers: { ...headers, ...options.headers },
        });

        if (!response.ok) {
            const errorData = await response.json().catch(() => ({}));
            throw new Error(errorData.message || 'Une erreur est survenue');
        }

        return response.json();
    },

    async get(endpoint, options = {}) {
        return this.request(endpoint, { ...options, method: 'GET' });
    },

    async post(endpoint, body = null, options = {}) {
        const requestOptions = { ...options, method: 'POST' };
        if (body && !(body instanceof FormData)) {
            requestOptions.body = JSON.stringify(body);
        } else {
            requestOptions.body = body;
        }
        return this.request(endpoint, requestOptions);
    },

    async put(endpoint, body = null, options = {}) {
        const requestOptions = { ...options, method: 'PUT' };
        if (body && !(body instanceof FormData)) {
            requestOptions.body = JSON.stringify(body);
        } else {
            requestOptions.body = body;
        }
        return this.request(endpoint, requestOptions);
    },

    async delete(endpoint, options = {}) {
        return this.request(endpoint, { ...options, method: 'DELETE' });
    }
};

export default api;