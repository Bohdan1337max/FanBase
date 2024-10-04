import React, {useState} from "react";
import { useNavigate} from "react-router-dom";


const SignUpPage = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [userName, setUserName] = useState('');
    const [userData, setUserData] = useState();
    const apiUrl = "http://localhost:5000/api/auth/signUp";
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault();
    }
    const signUpButtonHandler = async () => {
        const requestOption = {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({
                userName: userName,
                email: email,
                password: password
            })
        }

        const response = await fetch(apiUrl, requestOption);

        if (!response.ok)
            console.error(await response.json());

        const data = await response.json();

        const token = data.token;

        localStorage.setItem('jwtToken', token);
        navigate('/')

    }

    return (
        <div className={"login-container"}>
            <form>
                <h2>Fill data</h2>
                <div>
                    <label>User Name:</label>
                    <input
                        onChange={(e) => setUserName(e.target.value)}
                    />
                </div>
                <div>
                    <label>Email:</label>
                    <input
                        onChange={(e) => setEmail(e.target.value)}
                    />
                    <label>Password:</label>
                    <input
                        onChange={(e) => setPassword(e.target.value)}
                    />
                </div>
                <button onClick={signUpButtonHandler} type={"button"}>Sign Up</button>
            </form>
        </div>
    )
}

export default SignUpPage;