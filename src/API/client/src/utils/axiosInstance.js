import axios from 'axios';

const API_URL = 'https://localhost:5001';

const $api = axios.create({
    baseUrl: API_URL,
});

$api.interceptors.request.use((req) => {
    req.headers.Authorization = `Bearer ${localStorage.getItem('accessToken')}`;
    return req;
});

$api.interceptors.response.use(
    (response) => {
        return response;
    },
    async (error) => {
        const originalResponse = error.config;
        if(error.response) {
            if(error.response.status === 401 && !originalResponse._retry) {
                originalResponse._retry = true;

                const result = await axios.post(`${API_URL}/api/auth/refreshtoken`, {
                    token: localStorage.getItem('accessToken'),
                    refreshToken: localStorage.getItem('refreshToken')
                });

                if (result.jwtToken.success === true)
                {
                    localStorage.setItem('accessToken', result.jwtToken.token);
                    localStorage.setItem('refreshToken', result.jwtToken.refreshToken);

                    originalResponse.headers.Authorization = `Bearer ${localStorage.getItem('accessToken')}`;
                    return originalResponse;
                }
                else {
                    window.location.href = "/login";
                }

            }
        }
        return Promise.reject(error);
    }
);

export default $api;
