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
import TokenName from '../components/token-name-const.js';


const Game = () => {
  const token = Cookies.get(TokenName);
  
  const navigate = useNavigate();

  const uid = useRef();
  const [playerName, setPlayerName] = useState('You');
  const [opponentName, setOpponentName] = useState('Sematary');
  const [timer, setTimer] = useState(7); // Время на ход 
  const [gameState, setGameState] = useState('waiting'); // 'waiting', 'playing', 'result'
  const [result, setResult] = useState('');
  const [connection, setConnection] = useState(null);
  const [messages, setMessages] = useState([]);
  const [message, setMessage] = useState([]);
  const [start, setStart] = useState('');
  const [disabledButtons, setDisabledButtons] = useState(false);
  const { roomId } = useParams();
  const [matchId, setMatchId] = useState('');
  const [username, setUsername] = useState('');
  const [joinButtonHidden, setJoinButtonHidden] = useState(true);



  const messagesRef = useRef(null);
  
  const CheckForCreator = () => {
      axiosInstance.get(`/gameroom_info?id=${roomId}`).then(res => {
        if(res.data.creatorId != uid.current){
          setJoinButtonHidden(false);
        }
      })
  }
  

  //пшол отсюда
  useEffect(() => {
    if (!token){
        navigate("/sign-in");
    }
  }, [navigate, token])

  useEffect(()=> {
    if (token !== undefined && token !== null) {
      const decoded = jwtDecode(token);
      uid.current = decoded.Id;
      axiosInstance.get(`/userinfo?id=${uid.current}`,
      {
          headers:{
              Authorization: `Bearer ${token}`,
              Accept : "application/json"
          }
      }).then(response => { 
        setUsername(response.data.UserName)
      })
    }
    CheckForCreator();
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
    let con = new signalR.HubConnectionBuilder().withUrl(`${ServerURL}/gameRoomHub`).build();
  
    con.start().then(res => {
        con.invoke("GetGroupMessages", `${roomId}`)
                .catch(function (err) {
            return console.error(err.toString());
        });
    });
    
    con.on("ReceivePrivateMessage", function (user, message){
        let newMessage = 
        {
            belongsToSender : user === username,
            message : message,
            senderName : user
        };
        setMessages(prev => [...prev, newMessage])
    });
    
    con.on("GameStarted", function(res){
          setMatchId(res.data);
          setGameState('playing');
    });
    
    con.on("PlayerDisconnect", function(){
          setGameState('waiting');
      });

    con.on("ReceiveTimer", function (tick){
          setTimer(tick);});

    let winner = '';
    con.on("ReceiveGameResult", function (res){
            winner = res.data
            const resultMessage = determineResult(winner);
            endGame(resultMessage);
    });
            
    setConnection(con);
  }, [username])
  
  useEffect(() => { 
      callbackSignalR();
  }, [callbackSignalR, token])

  
  const endGame = (resultMessage) => {
    setGameState('result');
    setResult(resultMessage);
    setTimer(0);
    setDisabledButtons(true);
    // Другая логика завершения игры?
  };

  const determineResult = (res) => {
    if(res === uid.current) {
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

  const makeMove = (playerMove) => {
    // Логика выполнения хода, отправка на сервер, получение результата
    connection.invoke("MakeMove", matchId, playerMove, uid.current);
  };

  return (
    <>
        <Header />
        <div className='main-container'>
          <button
          hidden={joinButtonHidden}
          onClick={() => {
            connection.invoke("JoinLobby", roomId, uid.current);
            setStart('start');
          }}>Join</button>
        <div className="game-container">
            <div className="opponent-name">
              Opponent: {gameState === 'playing'? opponentName : ''}</div>
            <div className="timer">{gameState === 'result' ? 0 :timer}s</div>

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
