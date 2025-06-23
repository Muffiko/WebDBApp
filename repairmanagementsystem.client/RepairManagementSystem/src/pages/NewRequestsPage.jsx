import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import NewRequestsList from "../components/NewRequestsList";
import "./styles/NewRequestsPage.css";
import { useRepairObjectTypesApi } from "../api/repairObjectTypes";
import { useRepairRequestApi } from "../api/repairRequests";

const NewRequestsPage = () => {
  const [filters, setFilters] = useState({ name: "", type: "" });

  const { getRepairObjectTypes } = useRepairObjectTypesApi();
  const [objectTypes, setObjectTypes] = useState([]);

  const loadObjectTypes = async () => {
    try {
      const data = await getRepairObjectTypes();
      setObjectTypes(data);
    } catch (error) {
      console.error("Error loading object types:", error);
    }
  };

  const { getUnassignedRepairRequests } = useRepairRequestApi();
  const [requests, setRequests] = useState([]);

  const loadRepairRequests = async () => {
    try {
      const data = await getUnassignedRepairRequests();
      const mapped = data
        .filter(r => r.repairObjectName)
        .map((r) => ({
          repairRequestId: r.repairRequestId,
          name: r.repairObjectName || "(no name)",
          type: r.repairObjectType?.name || "",
          createdAt: r.createdAt ? new Date(r.createdAt).toLocaleDateString() : '',
          description: r.description,
        }));
      setRequests(mapped);
    } catch (err) {
      console.error("Failed to load requests:", err);
    }
  };

  useEffect(() => {
    loadObjectTypes();
    loadRepairRequests();
  }, []);

  const { updateManagerRepairRequest } = useRepairRequestApi();
  const { id } = useParams();
  const navigate = useNavigate();

  const handleManagerAssign = async (repairRequestId, managerId) => {
    try {
      await updateManagerRepairRequest(repairRequestId, managerId);
      setRequests(prev =>
        prev.map(r =>
          r.repairRequestId === repairRequestId
            ? { ...r, managerId: managerId }
            : r
        )
      );
      navigate(`/manage-request/${repairRequestId}`);
    } catch (error) {
      console.error("Failed to assign manager or update status:", error);
    }
  };

  const typesOptions = objectTypes
    .map((a) => a.name)
    .sort((a, b) => a.localeCompare(b));

  const filtered = requests.filter(
    (r) =>
      r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
      (filters.type === "" || r.type === filters.type)
  );

  return (
    <div className="requests-container">
      <Sidebar />
      <main className="requests-page">
        <h1 className="requests-title">New requests</h1>

        <FilterBar
          filters={filters}
          setFilters={setFilters}
          createLabel={null}
          selects={[
            {
              key: "type",
              label: "Type:",
              options: typesOptions,
            },
          ]}
        />
        <NewRequestsList newRequests={filtered} onAssign={handleManagerAssign} />
      </main>
    </div>
  );
};

export default NewRequestsPage;