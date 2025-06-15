import { useAuthFetch } from "./authFetch";

export const useRepairActivityTypesApi = () => {
  const authFetch = useAuthFetch();

  const getRepairActivityTypes = async () => {
    const response = await authFetch("/RepairActivityTypes", {
      method: "GET",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  return {
    getRepairActivityTypes,
  };
};