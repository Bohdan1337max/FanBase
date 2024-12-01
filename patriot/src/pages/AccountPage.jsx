import React, {useEffect, useState} from "react";
import {Logout} from '../services/loginService';
import {useNavigate} from "react-router-dom";

const AccountPage = () => {
    const [accountData, setAccountData] = useState(null);
    const [tiersData, setTiersData] = useState([]);
    const storedToken = localStorage.getItem('jwtToken');
    console.log(storedToken)

    useEffect(() => {
        if (storedToken) {
            const fetchData = async () => {
                try {
                    const response = await fetch('http://localhost:5000/api/auth/show-user-info', {
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
                    console.log(data.id)
                    const tiersResponse = await fetch(`http://localhost:5000/api/subscriptions/tiers?creatorId=${data.id}`, {
                        method: 'GET',
                        headers: {
                            'Authorization': `Bearer ${storedToken}`,
                            'Content-Type': 'application/json'
                        }
                    });
                    if (!tiersResponse.ok) {
                        throw new Error('Failed to fetch tiers data');
                    }
                    const tiersData = await tiersResponse.json();
                    setTiersData(tiersData);
                } catch (error) {
                    console.error('Error fetching account data:', error);
                }
            };
            fetchData();
        }
    }, [storedToken]);


    if (!accountData) {
        return <div>Loading...</div>;
    }

    return (
        <div>
            <div style={{display: "flex", flexDirection: "column", alignItems: "center"}}>

                <h1 style={{alignSelf: "self-start"}}>Account Page</h1>

                <img src={`http://localhost:5000${accountData.profileImageUrl}`} alt="Profile"
                     style={{width: '200px', height: '200px', marginBottom:"20px"}}/>

                <div style={{width: "500px"}}>

                    <div style={{
                        display: "flex",
                        flexDirection: "row",
                        justifyContent: "space-between",
                        marginBottom: "10px"
                    }}>
                        <div>
                            <div>Account name:</div>
                            <div>{accountData.userName}</div>
                        </div>
                        <button>Edit</button>
                    </div>

                    <div style={{
                        display: "flex",
                        flexDirection: "row",
                        justifyContent: "space-between",
                        marginBottom: "10px"
                    }}>
                        <div>
                            <div>Email:</div>
                            <div>{accountData.email}</div>
                        </div>

                        <button>Edit</button>
                    </div>

                    <div style={{display: "flex", flexDirection: "row", justifyContent: "space-between"}}>
                        <div>
                            <div>Description:</div>
                            <div>{accountData.bio}</div>
                        </div>
                        <button> Edit</button>
                    </div>
                </div>
            </div>
            <SubscriptionTiers tiersData={tiersData}></SubscriptionTiers>
        </div>
    )
}

const SubscriptionTiers = ({tiersData}) => {

    const navigate = useNavigate();
    const handleLogoutButton = () => {
        Logout();
        navigate('/')
    }
    return (
        <div>
            <h3>Subscription Tiers</h3>
            {tiersData.length === 0 ? (
                <p>No tiers available</p>
            ) : (
                <table>
                    <thead>
                    <tr>
                        <th>Name</th>
                        <th>Price</th>
                        <th>Description</th>
                    </tr>
                    </thead>
                    <tbody>
                    {tiersData.map(tier => (
                        <tr key={tier.id}>
                            <td>{tier.name}</td>
                            <td>{tier.price}</td>
                            <td>{tier.description}</td>
                        </tr>
                    ))}
                    </tbody>
                </table>
            )}
            <button onClick={handleLogoutButton}> Log out</button>
        </div>
    )
}

export default AccountPage