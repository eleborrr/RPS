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
import TokenName from '../components/token-name-const.js';

const CreateGame = () => {
    const token = Cookies.get(TokenName);
    const navigate = useNavigate();
    const uid = useRef();
    const [maxRating, setMaxRating] = useState(0);
    const [username, setUsername] = useState('');
    

    //пшол отсюда
    useEffect(() => {
        if (!token){
            navigate("/sign-in");
        }
    }, [navigate, token])

    useEffect(()=> {
        if (token !== undefined && token !== null) {
            const decoded = jwtDecode(token);
            console.log(`VOT ${decoded.Id}`)
            uid.current = decoded.Id;
            console.log(uid.current);
            axiosInstance.get(`/userinfo?id=${uid}`,
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

    const roomData = {
        maxRating: maxRating, 
        userId: uid.current, 
      };

    const handleSubmit = (e) => {

        e.preventDefault();

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