import '../styles/game.css';

const Message = ({ senderName, message, belongsToSender }) => {

    return (
        <div className={belongsToSender ? "chat-message" : "chat-message-your"}>{`${senderName}: ${message}`}</div>
    )
}
export default Message;