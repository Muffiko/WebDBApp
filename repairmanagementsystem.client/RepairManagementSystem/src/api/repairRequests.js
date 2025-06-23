import { useAuthFetch } from "./authFetch";

export const useRepairRequestApi = () => {
  const authFetch = useAuthFetch();

  const getCustomerRepairRequests = async () => {
    const response = await authFetch("/RepairRequests/customer/my", {
        method: "GET",
    });
    if (!response.ok) {

      const err = await response.json();
      throw err;
    }
    return await response.json();
  };

  const addRepairRequest = async (data) => {
    const response = await authFetch("/RepairRequests", {
      method: "POST",
      body: JSON.stringify(data),
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  const getRepairRequestById = async (id) => {
    const response = await authFetch(`/RepairRequests/${id}`, {
      method: "GET",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  const getRepairRequests = async () => {
    const response = await authFetch("/RepairRequests", {
      method: "GET",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  const getUnassignedRepairRequests = async () => {
    const response = await authFetch("/RepairRequests/unassigned", {
      method: "GET",
    });
    if (!response.ok) {
      const err = await response.json();
      throw err;
    }
    return await response.json();
  };

  const getActiveRepairRequests = async () => {
    const response = await authFetch("/RepairRequests/active", {
      method: "GET",
    });
    if (!response.ok) {
      const err = await response.json();
      throw err;
    }
    return await response.json();
  };

  const getFinishedRepairRequests = async () => {
    const response = await authFetch("/RepairRequests/finished", {
      method: "GET",
    });
    if (!response.ok) {
      const err = await response.json();
      throw err;
    }
    return await response.json();
  };

  const updateManagerRepairRequest = async (repairRequestId, managerId) => {
    const response = await authFetch(`/RepairRequests/${repairRequestId}/assign`, {
      method: "PATCH",
      body: JSON.stringify({ managerId }),
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  const changeRepairRequestStatus = async (repairRequestId, newStatus, result) => {
    const response = await authFetch(`/RepairRequests/${repairRequestId}/change-status`, {
      method: "PATCH",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({ newStatus, result }),
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  return { 
    addRepairRequest,
    getCustomerRepairRequests,
    getRepairRequestById,
    getRepairRequests,
    getUnassignedRepairRequests,
    getActiveRepairRequests,
    updateManagerRepairRequest,
    changeRepairRequestStatus,
    getFinishedRepairRequests
  };
};
