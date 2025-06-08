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
  }

  return { 
    addRepairRequest,
    getCustomerRepairRequests,
    getRepairRequestById
   };
};
