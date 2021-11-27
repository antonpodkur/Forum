import React, {useContext, useState} from 'react';
import {Context} from "../index";
import {observer} from "mobx-react-lite";

const RegisterPage = () => {
    const [username, setUsername] = useState('');
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const {userStore} = useContext(Context);

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
            <button onClick={e => userStore.register(email, password)}>Log in</button>
        </div>
    );
};

export default observer(RegisterPage);