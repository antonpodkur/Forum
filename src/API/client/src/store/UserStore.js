import {makeAutoObservable} from "mobx";

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
}
const userStore = new UserStore();
export default userStore;