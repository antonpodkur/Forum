import {$api, API_URL} from '../utils/axiosInstance';

class AuthService {
    async login(email, password) {
        try{
            const response = await $api.post(`${API_URL}/api/auth/login`, {email, password});
            localStorage.setItem('accessToken', response.data.jwtToken.token);
            localStorage.setItem('refreshToken', response.data.jwtToken.refreshToken);
            return response.data.user;
        }catch (e) {
            console.log(e.message);
        }
    }

    async register(username, email, password) {
        try {
            const response = await $api.post(`${API_URL}/api/auth/register`, {username, email, password});
            localStorage.setItem('accessToken', response.data.jwtToken.token);
            localStorage.setItem('refreshToken', response.data.jwtToken.refreshToken);
            return response.data.user;
        } catch (e) {
            console.log(e.message);
        }
    }

    async logout() {
        try{
            await $api.post(`${API_URL}/api/auth/logout`);
            localStorage.removeItem('accessToken');
            localStorage.removeItem('refreshToken');
            return {};
        } catch(e) {
            console.log(e.message);
        }
    }
}

const authService = new AuthService();

export default authService;