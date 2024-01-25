import React from "react";
import { BrowserRouter as Router, Routes, Route, NavLink } from "react-router-dom";
import SignUpForm from "./pages/sign-up";
import SignInForm from "./pages/sign-in";

import "./App.css";

const App = () => {
  return (
    <Router basename="/">
      <div className="App">
        <div className="appAside" />
        <div className="appForm">
          <div className="pageSwitcher">
            <NavLink
              to="/sign-in" 
              className="pageSwitcherItem"
            >
              Sign In
            </NavLink>
            <NavLink
              to="/"
              className="pageSwitcherItem"
            >
              Sign Up
            </NavLink>
          </div>

          <div className="formTitle">
            <NavLink
              to="/sign-in"
              className="formTitleLink"
            >
              Sign In
            </NavLink>{" "}
            or{" "}
            <NavLink
              to="/"
              className="formTitleLink"
            >
              Sign Up
            </NavLink>
          </div>

          <Routes>
            <Route path="/" element={<SignUpForm />} />
            <Route path="/sign-in" element={<SignInForm />} />
          </Routes>
        </div>
      </div>
    </Router>
  );
};

export default App;
