import { useAuth } from "../contexts/AuthContext";
import { jwtDecode } from "jwt-decode";

const API_URL = "http://localhost:5062/api";

export const useAuthFetch = () => {
  const { accessToken, login, logout } = useAuth();

  const authFetch = async (url, options = {}) => {
    let token = accessToken;

    const makeRequest = async (tokenToUse) => {
      const headers = {
        ...(options.headers || {}),
        Authorization: `Bearer ${tokenToUse}`,
        "Content-Type": "application/json",
      };

      const res = await fetch(`${API_URL}${url}`, {
        ...options,
        headers,
        credentials: "include",
      });

      return res;
    };

    let response = await makeRequest(token);

    if (response.status === 401) {

      const refreshRes = await fetch(`${API_URL}/Auth/refresh`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        credentials: "include",
        body: JSON.stringify({ refreshToken: token }),
      });

      if (!refreshRes.ok) {
        logout();
        throw new Error("Session expired. Please log in again.");
      }

      const data = await refreshRes.json();

      const decoded = jwtDecode(data.token);
      login(data.token, {
        email: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"],
        role: decoded["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"],
        firstName: decoded["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"]
      });

      response = await makeRequest(data.token);
    }

    return response;
  };

  return authFetch;
};
