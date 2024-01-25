import React, { useState } from "react";
import { Link, NavLink } from "react-router-dom";

const SignInForm = () => {
  const [formData, setFormData] = useState({
    email: "",
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

    console.log("The form was submitted with the following data:");
    console.log(formData);
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
          <label className="formFieldLabel" htmlFor="email">
            E-Mail Address
          </label>
          <input
            type="email"
            id="email"
            className="formFieldInput"
            placeholder="Enter your email"
            name="email"
            autoComplete="off"
            value={formData.email}
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
          <button type="submit" className="formFieldButton">
            Sign In
          </button>{" "}
          <Link to="/" className="formFieldLink">
            Create an account
          </Link>
        </div>
      </form>
    </div>
          
        </div>
      </div>
    
  );
};

export default SignInForm;
