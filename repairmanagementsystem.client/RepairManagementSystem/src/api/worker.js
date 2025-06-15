import { useAuthFetch } from "./authFetch";

export const useWorkersApi = () => {
  const authFetch = useAuthFetch();

  const getWorkers = async () => {
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
        getWorkers
    };
};