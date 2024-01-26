import React, { useState } from "react";
import { Link, NavLink } from "react-router-dom";
import Header from '../components/header';
import axiosInstance from "../components/axios_server";
import Cookies from "js-cookie";


const SignUpForm = () => {
  const [formData, setFormData] = useState({
    username: "",
    password: "",
    confirmPassword: ""
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
    
      axiosInstance
          .post('/registration', {
            UserName: formData.username,
            Password: formData.password,
            ConfirmPassword: formData.confirmPassword,
          }).then((res) => {
        console.log(res.data);
        if (!res.data.successful){
          console.log(res.data.message);
        }
        else{
          document.location.replace(`/sign-in`);
        }
      });

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
      <form className="formFields">
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
        <div className="formField">
          <label className="formFieldLabel" htmlFor="password">
            Confirm password
          </label>
          <input
            type="password"
            id="confirmPassword"
            className="formFieldInput"
            placeholder="Enter your password"
            name="confirmPassword"
            autoComplete="off"
            value={formData.confirmPassword}
            onChange={handleChange}
          />
        </div>
       
        

        <div className="formField">
          <button type="submit" onClick={handleSubmit} className="formFieldButton">
            Sign Up
          </button>{" "}
          <Link to="/sign-in" className="formFieldLink">
            I'm already a member
          </Link>
        </div>
      </form>
    </div>
    </div>
      </div>
    
  );
};

export default SignUpForm;