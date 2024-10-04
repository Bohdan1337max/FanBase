const SubscriptionTierCard = ({tier}) => {

    const handleJoinButton = () =>
    {

    }
    return (
        <div>
            <h3>{tier.name}</h3>
            <p>{tier.description}</p>
            <p>Price: ${tier.price}</p>
            <button>Join</button>
        </div>

    )

}

export default SubscriptionTierCard;