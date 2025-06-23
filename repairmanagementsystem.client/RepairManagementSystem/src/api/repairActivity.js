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

  const updateRepairActivity = async (id, data) => {
    const response = await authFetch(`/RepairActivities/${id}`, {
      method: "PATCH",
      body: JSON.stringify(data),
    });
  
    if (!response.ok) {
      const errData = await response.json();
      throw errData;
    }
  
    return await response.json();
  };

  const updateWorkerRepairActivity = async (repairActivityId, workerId) => {
    const response = await authFetch(`/RepairActivities/${repairActivityId}/assign`, {
      method: "PATCH",
      body: JSON.stringify({ workerId }),
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  return {
    getRepairActivities,
    addRepairActivity,
    updateRepairActivity,
    updateWorkerRepairActivity
  };
}