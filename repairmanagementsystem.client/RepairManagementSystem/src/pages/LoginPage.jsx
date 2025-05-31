// src/pages/LoginPage.jsx
import React from "react";
import LoginForm from "../components/LoginForm";
import { useAuth } from "../contexts/AuthContext";
import { Navigate } from "react-router-dom";
import "./styles/LoginPage.css";

const LoginPage = () => {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated) {
    return <Navigate to="/requests" replace />;
  }
  
  return (
    <div className="login-page">
      <LoginForm />
    </div>
  );
};

export default LoginPage;
