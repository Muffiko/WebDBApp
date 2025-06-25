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

  const getMyRepairActivities = async () => {
    const response = await authFetch("/RepairActivities/workers/my", {
      method: "GET",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  const getRepairActivityById = async (id) => {
    const response = await authFetch(`/RepairActivities/${id}`, {
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

  const changeRepairActivityStatus = async (id, statusData, token) => {
    const response = await authFetch(`/RepairActivities/${id}/change-status`, {
      method: "PATCH",
      headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${token}`, // Upewnij się, że token jest przekazany poprawnie
      },
      body: JSON.stringify(statusData),
    });

    if (!response.ok) {
      const errorText = await response.text();
      console.error("Error status:", response.status, "Error body:", errorText);
      throw new Error(errorText || `Request failed with status ${response.status}`);
    }

    // No JSON expected on success
    return;
  };

  
  return {
    getRepairActivities,
    getMyRepairActivities,
    getRepairActivityById,
    addRepairActivity,
    updateRepairActivity,
    updateWorkerRepairActivity,
    changeRepairActivityStatus, 
  };
};
