import React, {useState} from "react";

const NameSurnameInput = () => {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    return (
        <div className={"login-container"}>
            <form>
                <h2>Sign Up</h2>
                <h3>Write your name and surname</h3>
                <div>
                    <label>Name:</label>
                    <input
                        onChange={(e) => setEmail(e.target.value)}
                        required
                    />
                </div>
                <div>
                    <label>Surname (optional):</label>
                    <input
                        onChange={(e) => setPassword(e.target.value)}
                        autoComplete={"current-password"}
                        required
                    />
                </div>
                <button>Next</button>
            </form>
        </div>
    )
}

export default NameSurnameInput;