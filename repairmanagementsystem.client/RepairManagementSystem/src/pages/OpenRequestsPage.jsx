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

  const { getManagers } = useManagerApi();
  const [managers, setManagers] = useState([]);

  const loadManagers = async () => {
    try {
      const data = await getManagers();
      const mapped = data.map(m => ({
        managerId: m.managerId,
        firstName: m.user.firstName,
        lastName: m.user.lastName,
      }));
      setManagers(mapped);
    } catch (error) {
      console.error("Error loading managers:", error);
    }
  };

  const { getActiveRepairRequests } = useRepairRequestApi();
  const [requests, setRequests] = useState([]);

  const loadRepairRequests = async () => {
    try {
      const data = await getActiveRepairRequests();
      const mgrMap = managers.reduce((acc, m) => {
        acc[m.managerId] = `${m.firstName} ${m.lastName}`;
        return acc;
      }, {});

      const mapped = data.map((r) => ({
        repairRequestId: r.repairRequestId,
        name: r.repairObjectName,
        createdAt: new Date(r.createdAt).toLocaleDateString(),
        status: r.status,
        manager: r.managerId && mgrMap[r.managerId]
          ? mgrMap[r.managerId]
          : "Not assigned"
      }));
      setRequests(mapped);
    } catch (err) {
      console.error("Failed to load requests:", err);
    }
  };

  useEffect(() => {
    loadManagers();
  }, []);

  useEffect(() => {
    if (managers.length > 0) {
      loadRepairRequests();
    }
  }, [managers]);

  const managersOptions = managers.map(
    (m) => `${m.firstName} ${m.lastName}`
  );

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
            // {
            //   key: "status",
            //   label: "Status:",
            //   options: ["OPEN", "IN PROGRESS", "CLOSED", "CANCELLED", "COMPLETED", "TO DO"],
            // },
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