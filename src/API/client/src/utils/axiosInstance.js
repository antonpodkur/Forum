import axios from 'axios';

export const API_URL = 'http://localhost:5000';

export const $api = axios.create({
    baseUrl: API_URL
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

                try{
                    const result = await axios.post(`${API_URL}/api/auth/refreshtoken`,
                    {
                        token: localStorage.getItem('accessToken'),
                        refreshToken: localStorage.getItem('refreshToken')
                    });

                    console.log("hello");
                    console.log(result.data);

                    if (result.data.jwtToken.success === true)
                    {
                        localStorage.setItem('accessToken', result.data.jwtToken.token);
                        localStorage.setItem('refreshToken', result.data.jwtToken.refreshToken);

                        originalResponse.headers.Authorization = `Bearer ${localStorage.getItem('accessToken')}`;
                        return originalResponse;
                    }

                } catch(e) {
                    window.location.href = "/welcome";
                    localStorage.removeItem('accessToken');
                    localStorage.removeItem('refreshToken');
                }
            }
        }
        return Promise.reject(error);
    }
);