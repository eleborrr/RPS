import React from "react";
import { BrowserRouter as Router, Routes, Route, NavLink } from "react-router-dom";
import SignUpForm from "./pages/sign-up";
import SignInForm from "./pages/sign-in";
import Game from "./pages/game"

import "./App.css";

const App = () => {
  return (
    <Router basename="/">
      
      <Routes>
            <Route path='/game' element={<Game />} />
            <Route path="/sign-up" element={<SignUpForm />} />
            <Route path="/sign-in" element={<SignInForm />} />
          </Routes>
    </Router>
  );
};

export default App;
