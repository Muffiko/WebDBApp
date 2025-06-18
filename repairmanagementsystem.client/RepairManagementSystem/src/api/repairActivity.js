import { useAuthFetch } from "./authFetch";

export const useRepairActivityApi = () => {
  const authFetch = useAuthFetch();

  const getRepairActivities = async () => {
    const response = await authFetch("/RepairActivities", {
      method: "GET",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

   const addRepairActivity = async (data) => {
    const response = await authFetch("/RepairActivities", {
      method: "POST",
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  return {
    getRepairActivities,
    addRepairActivity
  };
}