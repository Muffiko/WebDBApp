import React, { createContext, useContext, useState, useEffect } from "react";
import { jwtDecode } from "jwt-decode";
import { refreshAccessToken } from "../api/auth";

export const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
  const [accessToken, setAccessToken] = useState(null);
  const [user, setUser] = useState(null);
  const [isAuthReady, setIsAuthReady] = useState(false);

  const login = (token, userData) => {
    setAccessToken(token);
    setUser(userData);
    localStorage.setItem("accessToken", token);
  };

  const logout = () => {
    setAccessToken(null);
    setUser(null);
    localStorage.removeItem("accessToken");
  };

  const restoreSession = async () => {
    const storedToken = localStorage.getItem("accessToken");
    if (!storedToken) {
      setIsAuthReady(true);
      return;
    }

    try {
      const decoded = jwtDecode(storedToken);
      const isExpired = decoded.exp * 1000 < Date.now();

      if (!isExpired) {
        setAccessToken(storedToken);
        setUser({
          email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
          role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
          firstName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
        });
      } else {
        const newToken = await refreshAccessToken(storedToken);
        if (newToken) {
          login(newToken.token, {
            email: newToken.email,
            role: newToken.role,
            firstName: newToken.firstName
          });
        } else {
          logout();
        }
      }
    } catch (err) {
      console.error("Session restoration failed:", err);
      logout();
    } finally {
      setIsAuthReady(true);
    }
  };

  useEffect(() => {
    restoreSession();
  }, []);

  useEffect(() => {
    const interval = setInterval(async () => {
      if (!accessToken) return;
  
      try {
        const decoded = jwtDecode(accessToken);
        const isExpired = decoded.exp * 1000 < Date.now() + 550000;
  
        if (isExpired) {
          console.log("🔄 Token expired, refreshing...");
          console.log("Current token:", accessToken);
          const newToken = await refreshAccessToken(accessToken);
          console.log("New token received:", newToken);
          if (newToken) {
            login(newToken.token, {
              email: newToken.email,
              role: newToken.role,
              firstName: newToken.firstName
            });
            console.log("🔁 Token refreshed");
          } else {
            logout();
          }
        } else {
          const timeLeft = Math.floor((decoded.exp * 1000 - Date.now()) / 1000);
          console.log(`🕒 Token still valid. Time left: ${timeLeft}s`);
        }
      } catch (err) {
        console.error("🔴 Auto-refresh failed:", err);
        logout();
      }
    }, 10000);
  
    return () => clearInterval(interval);
  }, [accessToken]);
  

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
