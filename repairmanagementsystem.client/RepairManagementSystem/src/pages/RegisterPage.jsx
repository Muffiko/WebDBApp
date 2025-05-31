// src/pages/RegisterPage.jsx
import React from "react";
import RegisterForm from "../components/RegisterForm";
import { useAuth } from "../contexts/AuthContext";
import { Navigate } from "react-router-dom";
import "./styles/RegisterPage.css";

const RegisterPage = () => {
  const { isAuthenticated } = useAuth();

  if (isAuthenticated) {
    return <Navigate to="/requests" replace />;
  }

  return (
    <div className="register-page">
      <RegisterForm />
    </div>
  );
};

export default RegisterPage;
