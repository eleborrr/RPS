import React, { useState } from "react";
import { Link, NavLink } from "react-router-dom";
import Cookies from 'js-cookie';
import axiosInstance from "../components/axios_server";

const SignInForm = () => {
  const [spanClass, setSpanClass] = useState('hide')
  const [errMessage, setErrMessage] = useState('')

  const [formData, setFormData] = useState({
    username: "",
    password: ""
  });

  const handleChange = (event) => {
    const target = event.target;
    const value = target.type === "checkbox" ? target.checked : target.value;
    const name = target.name;

    setFormData({
      ...formData,
      [name]: value
    });
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    axiosInstance.post('/login', {
      userName: formData.username,
      password: formData.password,
      rememberMe: true,
      returnUrl: ""
    }).then((res) => {
      console.log(res.data);
      if (!res.data.successful){
        setSpanClass('errorMessage');
        setErrMessage(res.data.message);
      }
      else{
        Cookies.set('token', res.data.message);
        document.location.replace(`/games`);
      }
    }) ;
  };

  return (
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
              to="/sign-up"
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
              to="/sign-up"
              className="formTitleLink"
            >
              Sign Up
            </NavLink>
          </div>
          <div className="formCenter">
      <form className="formFields" onSubmit={handleSubmit}>
        <div className="formField">
          <label className="formFieldLabel" htmlFor="username">
            Username
          </label>
          <input
            type="username"
            id="username"
            className="formFieldInput"
            placeholder="Enter your username"
            name="username"
            autoComplete="off"
            value={formData.username}
            onChange={handleChange}
          />
        </div>

        <div className="formField">
          <label className="formFieldLabel" htmlFor="password">
            Password
          </label>
          <input
            type="password"
            id="password"
            className="formFieldInput"
            placeholder="Enter your password"
            name="password"
            autoComplete="off"
            value={formData.password}
            onChange={handleChange}
          />
        </div>
        <form>
          <div className="formField">
            <button type="submit" onClick={handleSubmit} className="formFieldButton">
              Sign In
            </button>{" "}
            <Link to="/sign-up" className="formFieldLink">
              Create an account
            </Link>
          </div>
        </form>
      </form>
    </div>
          
        </div>
      </div>
    
  );
};

export default SignInForm;
