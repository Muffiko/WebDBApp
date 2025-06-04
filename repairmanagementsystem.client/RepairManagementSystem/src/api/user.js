import { useAuthFetch } from "./authFetch";

export const useUserApi = () => {
  const authFetch = useAuthFetch();

  const changePassword = async (data) => {
    const response = await authFetch("/Users/password-reset", {
      method: "POST",
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      const errData = await response.json();
      throw errData;
    }

    return await response.json();
  };

  const updateUserInfo = async (data) => {
    const response = await authFetch("/Users/update", {
      method: "PATCH",
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      const errData = await response.json();
      throw errData;
    }

    return await response.json();
  };

  const updateUserAddress = async (data) => {
    const response = await authFetch("/Users/address", {
      method: "PATCH",
      body: JSON.stringify(data),
    });
  
    if (!response.ok) {
      const errData = await response.json();
      throw errData;
    }
  
    return await response.json();
  };

  const getUserProfile = async () => {
    const res = await authFetch("/Users/me", {
      method: "GET",
    });
  
    if (!res.ok) {
      const err = await res.json();
      throw err;
    }
  
    return await res.json();
  };

  return {
    changePassword,
    updateUserInfo,
    updateUserAddress,
    getUserProfile,
  };
};
