import { useAuthFetch } from "./authFetch";

export const useRepairActivityApi = () => {
  const authFetch = useAuthFetch();

  const getRepairActivities = async (repairActivityId) => {
    const response = await authFetch(`/RepairActivities/${repairActivityId}`, {
      method: "GET",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  return {
    getRepairActivities
  };
}