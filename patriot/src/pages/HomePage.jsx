import React, {useState, useEffect} from 'react';
import {useNavigate} from 'react-router-dom';
import CreatorCard from "../components/CreatorCard";

const HomePage = () => {
    const [creators, setCreators] = useState([]);
    const navigate = useNavigate();


    useEffect(() => {
        const fetchCreators = async () => {
            try {
                const response = await fetch('http://localhost:5000/api/creators');
                if (!response.ok) {
                    throw new Error('Failed to fetch creator data');
                }
                const data = await response.json();
                setCreators(data);
            } catch (error) {
                console.error('Error fetching creator data:', error);
            }
        };

        fetchCreators();
    }, []);

    const handleLoginClick = () => {
        navigate('/login');
    };
    const handleSignUnClick = () => {
        navigate('signUp')
    }
    return (
        <div>
            <header className={"header"}>
                <h1>Welcome to the Patriot</h1>
                <div className={"control-container"}>
                    <button onClick={handleLoginClick}>Log In</button>
                    <button onClick={handleSignUnClick}>Sign Up</button>
                </div>

            </header>
            <div className={"body-container"}>
                {creators.length > 0 ? (
                    creators.map((creator) => <CreatorCard key={creator.id} creator={creator}/>)
                ) : (
                    <p> No creators available.</p>
                )}
            </div>
        </div>
    );
};

export default HomePage;
