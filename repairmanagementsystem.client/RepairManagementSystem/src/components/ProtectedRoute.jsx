import React from "react";
import { Navigate } from "react-router-dom";
import { useAuth } from "../contexts/AuthContext";

const ProtectedRoute = ({ allowedRoles, children }) => {
  const { user, isAuthenticated } = useAuth();

  if (!isAuthenticated) {
    return <Navigate to="/" replace />;
  }

  if (!allowedRoles.includes(user?.role)) {
    return <Navigate to="/profile" replace />;
  }

  return children;
};

export default ProtectedRoute;
