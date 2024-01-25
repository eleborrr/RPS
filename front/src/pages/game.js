import React, { useState, useEffect, useRef } from 'react';
import '../styles/game.css';
import Header from '../components/header';
import WaitingMessage from '../components/waiting-opponent';

const Game = () => {
  const [playerName, setPlayerName] = useState('You');
  const [opponentName, setOpponentName] = useState('Sematary');
  const [timer, setTimer] = useState(100); // Время на ход 
  const [gameState, setGameState] = useState('waiting'); // 'waiting', 'playing', 'result'
  const [result, setResult] = useState('');
  const [chat, setChat] = useState([]);
  const [disabledButtons, setDisabledButtons] = useState(false);

  useEffect(() => {
    const timerInterval = setInterval(() => {
      if (timer > 0 && gameState === 'playing') {
        setTimer((prevTimer) => prevTimer - 1);
      } else if (timer === 0 && gameState === 'playing') {
        endGame('Time is up!');
      }
    }, 1000);
    return () => clearInterval(timerInterval);
  }, [timer, gameState]);

  const messagesRef = useRef(null);
  
  useEffect(() => {
    messagesRef.current.scrollTop = messagesRef.current.scrollHeight;
  }, [chat]);

  const startGame = () => {
    setGameState('playing');
    setTimer(10); // set
    // Другая логика начала игры
  };

  const endGame = (resultMessage) => {
    setGameState('result');
    setResult(resultMessage);
    setTimer(0);
    setDisabledButtons(true);
    // Другая логика завершения игры
  };

  const makeMove = (playerMove) => {
    // Логика выполнения хода, отправка на сервер, получение результата
    const moves = ['rock', 'paper', 'scissors'];
    const opponentMove = moves[Math.floor(Math.random() * 3)];
    const resultMessage = determineResult(playerMove, opponentMove);
    endGame(resultMessage);
  };

  const determineResult = (playerMove, opponentMove) => {
    // Логика определения результата
    if (playerMove === opponentMove) return 'It\'s a tie!';
    if (
      (playerMove === 'rock' && opponentMove === 'scissors') ||
      (playerMove === 'paper' && opponentMove === 'rock') ||
      (playerMove === 'scissors' && opponentMove === 'paper')
    ) {
      return 'You win!';
    } else {
      return 'You lose!';
    }
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
            onClick={() => handleButtonClick('rock')}
            disabled={disabledButtons}
          >
            Rock
          </button>
          <button
            onClick={() => handleButtonClick('paper')}
            disabled={disabledButtons}
          >
            Paper
          </button>
          <button
            onClick={() => handleButtonClick('scissors')}
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
          {chat.map((message, index) => (
            <div className='chat-message' key={index}>{message}</div>
          ))}
        </div>
        <input className='chat-input'   
          type="text"
          placeholder="Type your message..."
          onKeyDown={(e) => {
            if (e.key === 'Enter') {
              setChat([...chat, `${playerName}: ${e.target.value}`]);
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
