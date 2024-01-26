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
import GameItem from '../components/game-item.js'

const AllGames = () => {
    const token = Cookies.get(TokenName);
    const navigate = useNavigate();
    let uid = '';
    const [rooms, setRooms] = useState([]);
    
    useEffect(() => {
        if (!token){
            navigate("/sign-in");
        }
    }, [navigate, token])

    useEffect(() => {
        axiosInstance.get('/allRooms',
         {
            headers:{
                Authorization: `Bearer ${token}`,
                Accept : "application/json"
            }
        })
        .then(res => {setRooms(res.data)})
        .catch(err => {
            console.log(err)}
            );
    }, [token])

    return(
        <>
            <Header />
            <div className="all-rooms">
                <h2>Все игровые комнаты</h2>
                <div className="rooms-container">
                    {rooms.map(room => (
                        <li onClick={() => navigate(`${room.gameRoomId}`)}>
                        <div className='content'>
                            <h3>{room.creatorName} {room.datetime}</h3>
                        </div>
                    </li>
                    ))}
                </div>
            </div>
        </>
    )
}   

export default AllGames;