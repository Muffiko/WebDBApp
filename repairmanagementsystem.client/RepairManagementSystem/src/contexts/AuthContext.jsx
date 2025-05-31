import React, { createContext, useContext, useState, useEffect } from "react";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [accessToken, setAccessToken] = useState(null);
  const [user, setUser] = useState(null); // { email, role, firstName }

  const login = (token, userData) => {
    setAccessToken(token);
    setUser(userData);
  };

  const logout = () => {
    setAccessToken(null);
    setUser(null);
  };

  const contextValue = {
    accessToken,
    user,
    login,
    logout,
    isAuthenticated: !!accessToken
  };

  return (
    <AuthContext.Provider value={contextValue}>
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
