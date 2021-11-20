import {$api, API_URL} from "../utils/axiosInstance";

class PostService {
    async getAll() {
        try{
            const posts = await $api.get(`${API_URL}/api/post`);
            return posts;
        } catch (e) {
            console.log(e.message);
        }
    }
}

const postService = new PostService();

export default postService;