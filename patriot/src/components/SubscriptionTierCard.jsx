import {jwtDecode} from "jwt-decode";

const SubscriptionTierCard = ({tier, creator}) => {
    const subscribeUrl = 'http://localhost:5000/api/subscriptions'
    const storedToken = localStorage.getItem('jwtToken')
    const decodedToken = jwtDecode(storedToken);
    const userId = decodedToken.nameid;
    const currentDate = new Date().toISOString();
    console.log("Tier is:" ,tier, "Creator is: ",creator, "UserId is :", userId, "CurrentDate is :",currentDate)
    const requestOption = {
        method: 'POST',
        headers: {
            'Authorization': `Bearer ${storedToken}`,
            'Accept': 'application/json',
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({
            subscriberId: userId,
            creatorId: creator.id,
            subscriptionTierId: tier.id,
            startDate: currentDate
        })
    }


    const handleJoinButton = async () => {
        try {
            const response = await fetch(subscribeUrl,requestOption);
            if (!response.ok) {
                throw new Error("Cant subscribe")
            }
            const data =  await response.json();
            console.log(data);
        } catch (error) {
            console.error("Fail to subscribe", error)
        }
    }
    return (
        <div style={{border: "solid black 2px",borderRadius: "10px", padding: "2rem", width:"10rem", display:"flex", flexDirection:"column", alignItems:"center"}}>
            <h3>{tier.name}</h3>
            <p>{tier.description}</p>
            <p>Price: ${tier.price}</p>
            <button onClick={handleJoinButton}>Join</button>
        </div>
    )
}

export default SubscriptionTierCard;