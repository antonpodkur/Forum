import {makeAutoObservable} from "mobx";
import authService from "../services/AuthService";

class UserStore {
    id;
    username;
    email;
    posts;

    constructor() {
        makeAutoObservable(this);
    }
    setUser(user) {
        this.id = user.id;
        this.username = user.username;
        this.email = user.email;
        this.posts = user.posts;
    }

    async login(email, password) {
        try {
            const user = await authService.login(email, password);
            this.setUser(user);
        }catch (e)
        {
            console.log(e.message);
        }
    }
}
const userStore = new UserStore();
export default userStore;