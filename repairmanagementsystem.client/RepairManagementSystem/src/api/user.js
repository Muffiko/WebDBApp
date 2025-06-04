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

  return {
    changePassword,
    updateUserInfo,
    updateUserAddress,
  };
};
