import React, {useContext, useState} from 'react';
import {Context} from "../index";
import {observer} from "mobx-react-lite";
import {useNavigate} from "react-router-dom";

const RegisterPage = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const {userStore} = useContext(Context);
    const navigate = useNavigate();

    const register = (username, email, password) => {
        try{
            userStore.register(username, email, password);
            navigate("/");
        } catch (e) {
            console.error(e.message);
        }
    }

    return (
        <div>
            <input
                type="text"
                value={username}
                placeholder={'Username'}
                onChange={e => setUsername(e.target.value)}
            />
            <input
                type="email"
                value={email}
                placeholder={'Email'}
                onChange={e => setEmail(e.target.value)}
            />
            <input
                type="password"
                value={password}
                placeholder={'Password'}
                onChange={e => setPassword(e.target.value)}
            />
            <button onClick={e => register(username ,email, password)}>Register</button>
        </div>
    );
};

export default observer(RegisterPage);