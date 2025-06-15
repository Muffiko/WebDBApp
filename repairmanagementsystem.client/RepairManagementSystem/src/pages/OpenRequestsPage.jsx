import React, { useState, useEffect, useRef } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import RepairsList from "../components/RepairsList";
import "./styles/OpenRequestsPage.css";
import { useManagerApi } from "../api/manager";
import { useRepairRequestApi } from "../api/repairRequests";

const OpenRequestsPage = () => {
  const [filters, setFilters] = useState({
    name: "",
    status: "",
    manager: "",
  });

  const selectRef = useRef();
  const [manager, setManager] = useState([]);
  const { getManagers } = useManagerApi();
  const [managers, setManagers] = useState([]);

  const loadManagers = async () => {
    try {
      const data = await getManagers();
      const onlyManagers = data.filter(m => m.role === "Manager");
      const mapped = onlyManagers.map((m, mdx) => ({
        id: mdx + 1,
        managerId: m.id,
        name: `${m.firstName} ${m.lastName}`,
        email: m.email,
      }));
      setManagers(mapped);
    } catch (error) {
      console.error("Error loading managers:", error);
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
        createdAt: new Date(r.createdAt).toLocaleDateString(),
        status: r.status,
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
    loadManagers();
    loadRepairRequests();
  }, []);

  const managersOptions = manager
    ? Array.from(new Set([manager, ...managers.map(m => m.name)]))
    : managers.map(m => m.name);

  const [showModal, setShowModal] = useState(false);

  const filtered = requests.filter(
    (r) =>
      r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
      (filters.status === "" || r.status === filters.status) &&
      (filters.manager === "" || r.manager === filters.manager)
  );

  return (
    <div className="open-requests-container">
      <Sidebar />
      <main className="open-requests-page">
        <h1 className="open-requests-title">Open Requests</h1>
        <FilterBar
          filters={filters}
          setFilters={setFilters}
          onCreate={() => setShowModal(true)}
          createLabel={null}
          selects={[
            {
              key: "status",
              label: "Status:",
              options: ["Open", "In progress", "Closed"],
            },
            {
              key: "manager",
              label: "Manager:",
              options: [managersOptions],
            },
          ]}
        />
        <RepairsList repairs={filtered} />
      </main>
    </div>
  );
};

export default OpenRequestsPage;