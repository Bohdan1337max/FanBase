import React, {useState} from 'react';
import {StoreJwt} from '../services/loginService';
import {useNavigate} from "react-router-dom";

const LoginPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState('');
    const [isLoading, setIsLoading] = useState(false);
    const apiUrl = 'http://localhost:5000/api/auth/logIn'
    const navigate = useNavigate();
    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        await logInButtonHandle();
    }
    const signUpButtonHandle = () => {
        navigate("/signUp");
    }
    const logInButtonHandle = async () => {
        setIsLoading(true)
        const requestOption = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                email: email,
                password: password
            })
        }
        const success  = await StoreJwt(apiUrl, requestOption)
        setIsLoading(false);
        if (success)
        {
            setEmail('');
            setPassword('');
            navigate("/account")
        } else {
            return <div> toke n eero </div>
        }
    }
    return (
        <div className="login-container">
            <form onSubmit={handleSubmit}>
                <h2>Login</h2>
                {error && <p style={{color: 'red'}}>{error}</p>}
                {isLoading && <p>Loading...</p>}
                <div>
                    <label>Email:</label>
                    <input
                        type="email"
                        value={email}
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Password:</label>
                    <input
                        type="password"
                        value={password}
                        onChange={(e) => setPassword(e.target.value)}
                        autoComplete={"current-password"}
                        required
                    />
                </div>
                <div>
                    <button onClick={logInButtonHandle} type={"submit"}>Login</button>
                    <button className={"control-container"} onClick={signUpButtonHandle}>Sign Up</button>
                </div>
            </form>
        </div>
    );
}
export default LoginPage;