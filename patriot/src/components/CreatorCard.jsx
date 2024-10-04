import {useNavigate} from "react-router-dom";

const CreatorCard = ({creator}) => {
    const navigate = useNavigate();

    const handleCardClick = () => {
        navigate(`/creator/${creator.id}`, { state: { creator } });
    }

    return (
        <div className={"creator-card"} onClick={handleCardClick}>
            <img src={`http://localhost:5000${creator.profileImageUrl}`}
                 alt={creator.userName}
                 className={"creator-image"}>
            </img>
            <h2>{creator.userName}</h2>
            <p>{creator.bio}</p>
        </div>
    );
}

export default CreatorCard;