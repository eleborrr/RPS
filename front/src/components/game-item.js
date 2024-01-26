

const GameItem = ({creatorName, datetime, gameId}) => {
    return (
        <div className="gameContainer">
            <div className="upper">
                <span>{gameId}</span>
                <span>{creatorName}</span>
            </div>
            <span>{datetime}</span>
        </div>
    )
}

export default GameItem;