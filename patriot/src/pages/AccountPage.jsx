import React, {useEffect, useState} from "react";
import {Logout} from '../services/loginService';
import {useNavigate} from "react-router-dom";
const AccountPage = () => {
    const [accountData, setAccountData] = useState(null);
    const navigate = useNavigate();
    const storedToken = localStorage.getItem('jwtToken');
    console.log(storedToken)

    useEffect(() => {
        if (storedToken) {
            const fetchData = async () => {
                try {
                    const response = await fetch('http://localhost:5000/api/auth/showUserInfo', {
                        method: 'GET',
                        headers: {
                            'Authorization': `Bearer ${storedToken}`,
                            'Content-Type': 'application/json'
                        }
                    });
                    if (!response.ok) {
                        throw new Error('Failed to fetch account data');
                    }
                    const data = await response.json();
                    setAccountData(data);
                } catch (error) {
                    console.error('Error fetching account data:', error);
                }
            };
            fetchData();
        }
    }, [storedToken]);

    const handleLogoutButton = () => {
        Logout();
        navigate('/')
    }
    if (!accountData) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <div>
                Account Page
                <p>{accountData.id}</p>
                <p>{accountData.userName}</p>
                <p>{accountData.email}</p>
            </div>
            <button onClick={handleLogoutButton}> Log out</button>
        </div>
    )
}


export default AccountPage