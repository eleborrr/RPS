import axiosInstance from "../components/axios_server.js";
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

const CreateGame = () => {
    const token = Cookies.get(TokenName);
    const navigate = useNavigate();
    const [uid, setUid] = useState('');
    const [maxRating, setMaxRating] = useState(0);
    const [username, setUsername] = useState('');
    

    //пшол отсюда
    useEffect(() => {
        if (!token){
            //navigate("/sign-in");
        }
    }, [navigate, token])

    useEffect(()=> {
        if (token !== undefined && token !== null) {
        setUid(jwtDecode(token).Id);
        axiosInstance.get(`account/userinfo?id=${uid}`,
        {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        }).then(response => { 
            setUsername(response.data.UserName)
            console.log(uid);
        })
        } 
    },[])

    const roomData = {
        maxRating: maxRating, 
        userId: uid, 
      };

    const handleSubmit = (e) => {
        e.preventDefault();
        console.log(uid);
        axiosInstance.post('/createRoom', roomData).then(res => {
            navigate(`/game/${res.data}`)
        }) 
        
      };

    return(
        <div>
            <h1>Создать игру</h1>
            <div>
            <form onSubmit={handleSubmit}>
                <label>
                    Максимальное рейтинг:
                    <input
                    type="number"
                    value={maxRating}
                    onChange={(e) => setMaxRating(e.target.value)}
                    />
                </label>
                <button type="submit">Создать</button>
            </form>
            </div>
        </div>
    )
}

export default CreateGame;