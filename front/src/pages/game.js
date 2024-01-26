import axiosInstance from "../components/axios_server";
import React, { useMemo, useCallback, useState, useEffect, useRef } from 'react';
import { useNavigate, useParams } from "react-router-dom";
import '../styles/game.css';
import Header from '../components/header';
import WaitingMessage from '../components/waiting-opponent';
import * as signalR from "@microsoft/signalr";
import Message from '../components/message';
import Cookies from "js-cookie";
import ServerURL from '../components/server_url';
import { jwtDecode } from 'jwt-decode';
import TokenName from '../components/token-name-const';


const Game = () => {
  const token = Cookies.get(TokenName);
  const navigate = useNavigate();
  const connection = useMemo(() => {
    return new signalR.HubConnectionBuilder().withUrl(`${ServerURL}/gameRoomHub`).build();
  }, []);

  const [uid, setUid] = useState('') 
  const [playerName, setPlayerName] = useState('You');
  const [opponentName, setOpponentName] = useState('Sematary');
  const [timer, setTimer] = useState(); // Время на ход 
  const [gameState, setGameState] = useState('waiting'); // 'waiting', 'playing', 'result'
  const [result, setResult] = useState('');

  const [messages, setMessages] = useState([]);
  const [message, setMessage] = useState([]);
  const [disabledButtons, setDisabledButtons] = useState(false);
  const { roomId } = useParams();
  const [username, setUsername] = useState('');

  const messagesRef = useRef(null);
  
  //пшол отсюда
  useEffect(() => {
    if (!token){
        navigate("/sign-in");
    }
  }, [navigate, token])

  useEffect(()=> {
    if (token !== undefined && token !== null) {
      setUid( jwtDecode(token).Id);
      axiosInstance.get(`account/userinfo?id=${uid}`,
      {
         headers:{
             Authorization: `Bearer ${token}`,
             Accept : "application/json"
         }
      }).then(response => { 
        setUsername(response.data.UserName)
      })
    }
        
},[])

  
  useEffect(() => {
    messagesRef.current.scrollTop = messagesRef.current.scrollHeight;
  }, [messages]);

  const handleSend = async (event) => {
        
    if (message === "")
            return;

    await callSendMessageSignalR();
    setMessage("");

    event.preventDefault();
}

const callSendMessageSignalR = async () =>{
  connection.invoke("SendPrivateMessage",
      username,
      message,
      roomId)
      .catch(function (err) {
          console.log("error sending message");
          return console.error(err.toString());
      });
}

  const callbackSignalR = useCallback(() => {
  
    connection.start().then(res => {
        connection.invoke("GetGroupMessages", `${roomId}`)
                .catch(function (err) {
            return console.error(err.toString());
        });
    });
    
    connection.on("ReceivePrivateMessage", function (user, message){
        let newMessage = 
        {
            belongsToSender : user === username,
            message : message,
            senderName : user
        };
        setMessages(prev => [...prev, newMessage])
    }).catch(function(err){return console.error(err.toString())});
    return () => {
      connection.stop()};
  }, [username])
  
  useEffect(() => { 
      callbackSignalR();
  }, [callbackSignalR, token])

  useEffect(() => {
    connection.on("ReceiveTimer", function (timer){
      setTimer(timer);
  }).catch(function(err){return console.error(err.toString())});
  }, [timer, gameState])

  const startGame = () => {
    setGameState('playing');
    setTimer(10); // set
  };

  const endGame = (resultMessage) => {
    setGameState('result');
    setResult(resultMessage);
    setTimer(0);
    setDisabledButtons(true);
    // Другая логика завершения игры?
  };

  const makeMove = (playerMove) => {
    // Логика выполнения хода, отправка на сервер, получение результата
    connection.invoke("MakeMove", roomId, playerMove, uid).catch(function (err) {
      return console.error(err.toString());
  });
  let winner = ''
  connection.on("ReceiveGameResult", function (res){
    winner = res.data.WinnerId
}).catch(function(err){return console.error(err.toString())});
    const resultMessage = determineResult(result);
    endGame(resultMessage);
  };

  const determineResult = (res) => {
    if(res === uid) {
      return "You win"
    }
    else if(res === '-1')
      return "Its tie"
    else
      return "You lost"
  };

  const handleButtonClick = (move) => {
    makeMove(move);
  };

  return (
    <>
        <Header />
        <div className='main-container'>
        <div className="game-container">
            <div className="opponent-name">
              Opponent: {gameState === 'playing'? opponentName : ''}</div>
            <div className="timer">{timer}s</div>

        {gameState === 'waiting' && (
        <WaitingMessage />
      )}

      {gameState === 'playing' && (
        <div className="moves">
          <button
            onClick={() => handleButtonClick('1')}
            disabled={disabledButtons}
          >
            Rock
          </button>
          <button
            onClick={() => handleButtonClick('2')}
            disabled={disabledButtons}
          >
            Paper
          </button>
          <button
            onClick={() => handleButtonClick('3')}
            disabled={disabledButtons}
          >
            Scissors
          </button>
        </div>
      )}

      {gameState === 'result' && (
        <div className="result">{result}</div>
      )}

      

      <div className="user-name">{playerName}</div>
    </div>
    <div className="chat">
        <div className="chat-messages" ref={messagesRef}>
          {messages.map((message, index) => (<Message
          key={index}
          senderName={message.senderName}
          message={message.message}
          belongsToSender={message.belongsToSender} />
          ))}
        </div>
        <input className='chat-input'   
          type="text"
          placeholder="Type your message..."
          onChange={(e) => setMessage(e.target.value)}
          onKeyDown={(e) => {
            if (e.key === 'Enter') {
              handleSend(e);
              setMessages([...messages, `${playerName}: ${e.target.value}`]);
              e.target.value = '';
            }
          }}
        />
      </div>
      </div>
    </>
    
  );
};

export default Game;
