import axiosInstance from "../components/axios_server_mongo_service.js";
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
import GameItem from '../components/game-item.js'

const Ratings = () => {
    const token = Cookies.get(TokenName);
    const navigate = useNavigate();
    const [uid, setUid] = useState('')
    const [ratings, setRatings] = useState([]);

    useEffect(() => {
        if (!token){
            //navigate("/login");
        }
    }, [navigate, token])

    useEffect(() => {
        axiosInstance.get('/api/ratings')
            .then(res => {setRatings(res.data)})
            .catch(err => {
                console.log(err)}
            );
    }, [token])

    return(
        <>
            <Header />
            <div className="all-rooms">
                <h2>Ratings</h2>
                <div className="rooms-container">
                    {ratings.map(rating => (
                        <div className='content'>
                            <h3>{rating.userId} {rating.rating}</h3>
                        </div>
                    ))}
                </div>
            </div>
        </>
    )
}

export default Ratings;