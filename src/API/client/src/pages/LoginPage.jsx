import React, {useContext, useState} from 'react';
import {Context} from "../index";

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const {userStore} = useContext(Context);

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
            <button onClick={e => userStore.login(email, password)}>Log in</button>
        </div>
    );
};

export default LoginPage;