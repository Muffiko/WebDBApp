import React, { createContext, useContext, useState, useEffect } from "react";
import { jwtDecode } from "jwt-decode";
import { refreshAccessToken, logoutUser } from "../api/auth";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [accessToken, setAccessToken] = useState(null);
  const [user, setUser] = useState(null);
  const [isAuthReady, setIsAuthReady] = useState(false);

  const login = (token) => {
    setAccessToken(token);
    const decoded = jwtDecode(token);
    setUser({
      email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
      role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
      firstName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
    });
  };

  const logout = async () => {
    if (accessToken) {
      try {
        await logoutUser(accessToken);
      } catch (err) {
        console.warn("Server logout failed or already expired.");
      }
    }
  
    setAccessToken(null);
    setUser(null);
  };
  
  

  const restoreSession = async () => {
    try {
      const newToken = await refreshAccessToken();
      if (newToken) {
        login(newToken);
      }
    } catch (err) {
      console.warn("🛑 Session restoration failed.");
      logout();
    } finally {
      setIsAuthReady(true);
    }
  };

  useEffect(() => {
    restoreSession();
  }, []);

  return (
    <AuthContext.Provider
      value={{
        accessToken,
        user,
        login,
        logout,
        isAuthenticated: !!accessToken,
        isAuthReady
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export const useAuth = () => useContext(AuthContext);
