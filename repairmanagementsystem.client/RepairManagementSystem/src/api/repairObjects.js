import { useAuthFetch } from "./authFetch";

export const useRepairObjectApi = () => {
  const authFetch = useAuthFetch();

  const getCustomerRepairObjects = async () => {
    const res = await authFetch("/RepairObjects/customer/my", { 
      method: "GET" 
    });

    if (!res.ok) {
      const err = await res.json();
      throw err;
    }

    return await res.json();
  };

  const addRepairObject = async ({ name, repairObjectTypeId }) => {
    const res = await authFetch("/RepairObjects", {
      method: "POST",
      body: JSON.stringify({ name, repairObjectTypeId }),
    });

    if (!res.ok) {
      const err = await res.json();
      throw err;
    }

    return await res.json();
  };

  return { addRepairObject,
    getCustomerRepairObjects
   };
};