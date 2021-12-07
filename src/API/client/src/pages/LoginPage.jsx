import React, {useContext, useState} from 'react';
import {Context} from "../index";
import {observer} from "mobx-react-lite";
import {Link, useNavigate} from "react-router-dom";

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const {userStore} = useContext(Context);
    const navigate = useNavigate();


    const login = (email, password) => {
        try{
            userStore.login(email,password);
            navigate("/");
        } catch(e) {
            console.log(e.message);
        }

    }

    return (
        <div>
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
            <button onClick={e => login(email, password)}>Log in</button>
        </div>
    );
};

export default observer(LoginPage);