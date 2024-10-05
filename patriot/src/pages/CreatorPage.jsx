import {useLocation} from "react-router-dom";
import {useEffect, useState} from "react";
import SubscriptionTierCard from "../components/SubscriptionTierCard";

const CreatorPage = () => {
    const {state} = useLocation();
    const {creator} = state || {};
    const [tiers, setTiers] = useState([]);

    useEffect(() => {
        if (creator) {
            const fetchTiers = async () => {
                try {
                    const response = await fetch(`http://localhost:5000/api/subscription/getTiers?creatorId=${creator.id}`, {
                        method: 'GET',
                        headers: {
                            'Content-Type': 'application/json',
                        },
                    });
                    if (!response.ok) {
                        throw new Error('Failed to fetch subscription tiers');
                    }
                    const data = await response.json();
                    setTiers(data);
                } catch (error) {
                    console.error('Error fetching subscription tiers:', error);
                }
            };

            fetchTiers();
        }
    }, [creator]);

    if (!creator) {
        return <div>Loading creator data...</div>;
    }
    return (

        <div>
            <h1>{creator.userName}'s Profile</h1>
            <img
                src={`http://localhost:5000${creator.profileImageUrl}`}
                alt={creator.userName}
                style={{ width: '200px', height: '200px' }}
            />
            <p>{creator.bio}</p>

            <h2>Subscription Tiers</h2>
            <div>
                {tiers.length > 0 ? (
                    tiers.map((tier) => (
                        <SubscriptionTierCard key={tier.id} tier={tier} creator={creator}/>
                    ))
                ) : (
                    <p>No subscription tiers available for this creator.</p>
                )}
            </div>
        </div>
    );
}

export default CreatorPage