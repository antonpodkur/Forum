import {$api, API_URL} from '../utils/axiosInstance';

class AuthService {
    async login(email, password) {
        try{
            const response = await $api.post(`${API_URL}/api/auth/login`, {email, password});
            localStorage.setItem('accessToken', response.data.jwtToken.token);
            localStorage.setItem('refreshToken', response.data.jwtToken.refreshToken);
            return response.data.user;
        }catch (e)
        {
            console.log(e.message);
        }
    }
}

const authService = new AuthService();

export default authService;