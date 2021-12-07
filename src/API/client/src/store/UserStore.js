import {makeAutoObservable} from "mobx";
import authService from "../services/AuthService";

class UserStore {
    id;
    username;
    email;
    posts;

    isAuth;

    constructor() {
        makeAutoObservable(this);
    }

    setUser(user) {
        this.id = user.id;
        this.username = user.username;
        this.email = user.email;
        this.posts = user.posts;
    }

    setAuth(bool) {
        this.isAuth = bool;
    }

    async login(email, password) {
        try {
            const user = await authService.login(email, password);
            this.setUser(user);
            this.setAuth(true);
        }catch (e)
        {
            this.setAuth(false);
            console.log(e.message);
        }
    }

    async register(username, email, password) {
        try {
            const user = await authService.register(username,email,password);
            this.setUser(user);
            this.setAuth(true);
        } catch(e) {
            this.setAuth(false);
            console.log(e.message);
        }
    }

    async logout() {
        try {
            const user = await authService.logout();
            this.setUser(user);
            this.setAuth(false);
        } catch(e) {
            this.setAuth(false);
            console.log(e.message);
        }
    }

    getUser() {
        return {id: this.id, username: this.username, email: this.email};
    }
}

export default UserStore;