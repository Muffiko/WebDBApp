// src/components/LoginForm.jsx
import React, { useState } from "react";
import "./styles/LoginForm.css";

const LoginForm = () => {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");

  const handleLogin = (e) => {
    e.preventDefault();
    console.log("Logging in:", { email, password });
  };

  return (
    <div className="login-card">
      <h1 className="login-title">RMS</h1>
      <p className="login-subtitle">Login</p>
      <form onSubmit={handleLogin} className="login-form">
        <input
          type="email"
          placeholder="Email"
          className="login-input"
          value={email}
          onChange={(e) => setEmail(e.target.value)}
        />
        <input
          type="password"
          placeholder="Password"
          className="login-input"
          value={password}
          onChange={(e) => setPassword(e.target.value)}
        />
        <button type="submit" className="login-button">Login</button>
      </form>
      <p className="login-create-account">Create account</p>
    </div>
  );
};

export default LoginForm;
