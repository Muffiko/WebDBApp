import { useAuthFetch } from "./authFetch";

export const useManagerApi = () => {
  const authFetch = useAuthFetch();

  const getManagers = async () => {
    const response = await authFetch("/Users", {
        method: "GET",
    });
    if (!response.ok) {

      const err = await response.json();
      throw err;
    }
    return await response.json();
  };

    return { 
        getManagers
    };
};