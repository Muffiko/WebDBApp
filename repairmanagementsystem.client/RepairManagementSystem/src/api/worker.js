import { useAuthFetch } from "./authFetch";

export const useWorkersApi = () => {
  const authFetch = useAuthFetch();

  const getWorkers = async () => {
    const response = await authFetch("/Users/workers", {
        method: "GET",
    });
    if (!response.ok) {

      const err = await response.json();
      throw err;
    }
    return await response.json();
  };

  const updateWorkerAvailability = async (workerId, isAvailable) => {
    const response = await authFetch(`/Users/workers/${workerId}/availability`, {
      method: "PATCH",
      body: JSON.stringify({ isAvailable }),
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

    return { 
        getWorkers,
        updateWorkerAvailability
    };
};