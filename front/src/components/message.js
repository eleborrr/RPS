import '../styles/game.css';

const Message = ({ senderName, message, belongsToSender }) => {

    return (
        <div className={belongsToSender ? "chat-message-your" : "chat-message"}>{`${senderName}: ${message}`}</div>
    )
}
export default Message;