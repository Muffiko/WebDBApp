import { useAuthFetch } from "./authFetch";

export const useRepairObjectTypesApi = () => {
  const authFetch = useAuthFetch();

  const getRepairObjectTypes = async () => {
    const response = await authFetch("/RepairObjectTypes", {
      method: "GET",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  return {
    getRepairObjectTypes,
  };
};