import React, { useState, useContext } from "react";
import "./styles/LoginForm.css";
import { loginUser } from "../api/auth";
import { AuthContext } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";
import { Link } from "react-router-dom";

const LoginForm = () => {
  const [formData, setFormData] = useState({ email: "", password: "" });
  const [error, setError] = useState("");
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleChange = (e) => {
    setFormData(prev => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setError("");
    try {
      const response = await loginUser(formData);
      login(response.token);
      navigate("/profile");
    } catch (err) {
      let message = "Login failed";

      if (err?.message && typeof err.message === "string") {
        try {
          const parsed = JSON.parse(err.message);
          message = parsed.message || parsed.title || message;
        } catch {
          message = err.message;
        }
      }

      setError(message);
    }
  };

  return (
      <div className="login-card">
        <h1 className="login-title">RMS</h1>
        <p className="login-subtitle">Login</p>
        <form onSubmit={handleSubmit} className="login-form">
          <label>Email</label>
          <input
              type="email"
              name="email"
              placeholder="Enter your email"
              value={formData.email}
              onChange={handleChange}
          />
          <label>Password</label>
          <input
              type="password"
              name="password"
              placeholder="Enter your password"
              value={formData.password}
              onChange={handleChange}
          />
          {error && <p className="login-error">{error}</p>}
          <button type="submit" className="general-button">Login</button>
          <button
              type="button"
              onClick={() => navigate("/register")}
              className="general-button"
          >Register
          </button>
        </form>
      </div>
  );
};

export default LoginForm;
