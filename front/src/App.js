import React from "react";
import { BrowserRouter as Router, Routes, Route, NavLink } from "react-router-dom";
import SignUpForm from "./pages/sign-up";
import SignInForm from "./pages/sign-in";
import Game from "./pages/game";
import Cookies from "js-cookie";
import jwtDecode from "jwt-decode";
import TokenName from "./components/token-name-const";

import "./App.css";
import AllGames from "./pages/all-games";

const App = () => {
  const token = Cookies.get(TokenName);

  return (
    <Router basename="/">
      
      <Routes>
            <Route path='/game/:roomId' element={<Game />} />
            <Route path='/games' />
            <Route path="/sign-up" element={<SignUpForm />} />
            <Route path="/sign-in" element={<SignInForm />} />
            <Route path="/all" element={<AllGames />} />
          </Routes>
    </Router>
  );
};

export default App;
