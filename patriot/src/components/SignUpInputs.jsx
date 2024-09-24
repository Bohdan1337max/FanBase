import React from "react";

const SignUpInputs = () => {

    return(
        <div className={"login-container"}>
            <form>
                <h2>Fill data</h2>
                <div>
                    <label>User Name:</label>
                    <input/>
                </div>
                <div>
                    <label>Email:</label>
                    <input/>
                    <label>Password:</label>
                    <input/>
                </div>
                <button>Sign Up</button>
            </form>
        </div>
    )
}
export default SignUpInputs