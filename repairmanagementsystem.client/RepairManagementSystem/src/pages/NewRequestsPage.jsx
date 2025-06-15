import React, { useState, useEffect } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import NewRequestsList from "../components/NewRequestsList";
import "./styles/NewRequestsPage.css";
import { useRepairActivityTypesApi } from "../api/repairActivityType";
import { useRepairRequestApi } from "../api/repairRequests";

const NewRequestsPage = () => {
  const [filters, setFilters] = useState({ name: "", type: "" });
  const [activityType, setActivityType] = useState();

  const { getRepairActivityTypes } = useRepairActivityTypesApi();
  const [activityTypes, setActivityTypes] = useState([]);

  const loadActivityTypes = async () => {
    try {
      const data = await getRepairActivityTypes();
      setActivityTypes(data);
    } catch (error) {
      console.error("Error loading activity types:", error);
    }
  };

  const { getRepairRequests } = useRepairRequestApi();
  const [requests, setRequests] = useState([]);

  const loadRepairRequests = async () => {
    try {
      const data = await getRepairRequests();
      const mapped = data.map((r) => ({
        repairRequestId: r.repairRequestId,
        name: r.repairObject.name,
        type: r.repairObject.repairObjectType.name,
        createdAt: new Date(r.createdAt).toLocaleDateString(),
        description: r.description,
        manager:
          r.manager && r.manager.firstName
            ? `${r.manager.firstName} ${r.manager.lastName}`
            : 'Not assigned'
      }));
      setRequests(mapped);
    } catch (err) {
      console.error("Failed to load requests:", err);
    }
  };

  useEffect(() => {
    loadActivityTypes();
    loadRepairRequests();
  }, []);

  const activitiesOptions = activityTypes
    ? Array.from(new Set([activityType, ...activityTypes.map(a => a.name)]))
    : activityTypes.map(a => a.name);


  const filtered = requests.filter(
    (r) =>
      r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
      (filters.type === "" || r.type === filters.type)
  );

  const handleAssign = (id, manager) => {
    console.log("Assigned", manager, "to request", id);
    setRequests(prev => prev.filter(r => r.id !== id));
  };

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
              options: [activitiesOptions],
            },
          ]}
        />
        <NewRequestsList newRequests={filtered} onAssign={handleAssign} />
      </main>
    </div>
  );
};

export default NewRequestsPage;