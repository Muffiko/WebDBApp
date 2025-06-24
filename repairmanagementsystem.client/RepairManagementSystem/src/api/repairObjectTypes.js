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

  const addRepairObjectType = async (repairObjectTypeId, name) => {
    const response = await authFetch("/RepairObjectTypes", {
      method: "POST",
      body: JSON.stringify({
        repairObjectTypeId,
        name
      }),
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return await response.json();
  };

  const deleteRepairObjectType = async (repairObjectTypeId) => {
    const response = await authFetch(`/RepairObjectTypes/${repairObjectTypeId}`, {
      method: "DELETE",
    });

    if (!response.ok) {
      const err = await response.json();
      throw err;
    }

    return true;
  };

  return {
    getRepairObjectTypes,
    addRepairObjectType,
    deleteRepairObjectType
  };
};
