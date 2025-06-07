import React, { useEffect, useState } from "react";
import Sidebar from "../components/Sidebar";
import RequestCard from "../components/RequestCard";
import FilterBar from "../components/FilterBar";
import NewRequestModal from "../components/NewRequestModal";
import { useRepairRequestApi } from "../api/repairRequests";
import "./styles/RequestsPage.css";

const menuItems = [
  { path: "/objects", label: "My repair objects", icon: "🧰" },
  { path: "/requests", label: "My requests", icon: "📋" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const RequestsPage = () => {
  const { getCustomerRepairRequests, createRequest } = useRepairRequestApi();

  const [requests, setRequests] = useState([]);
  const [filters, setFilters] = useState({
    name: "",
    status: "",
    manager: ""
  });
  const [showModal, setShowModal] = useState(false);

  const loadRequests = async () => {
    try {
      const data = await getCustomerRepairRequests();
      const mapped = data.map((r) => ({
        id: r.request_Id,
        name: "Unknown", //TODO: Replace with actual name if available
        status: r.status,
        manager: r.managerName ?? "Not set",
        dateCreated: new Date(r.createdAt).toLocaleDateString()
      }));
      setRequests(mapped);
    } catch (err) {
      console.error("Failed to load requests:", err);
    }
  };

  useEffect(() => {
    loadRequests();
  }, []);

  const filtered = requests.filter((r) =>
    r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
    (filters.status === "" || r.status === filters.status) &&
    (filters.manager === "" || r.manager === filters.manager)
  );

  const uniqueManagers = [...new Set(requests.map((r) => r.manager))];
  const uniqueStatuses = [...new Set(requests.map((r) => r.status))];

  return (
    <div className="requests-container">
      <Sidebar menuItems={menuItems} />
      <div className="requests-page">
        <h1 className="requests-title">My requests</h1>

        <FilterBar
          filters={filters}
          setFilters={setFilters}
          onCreate={() => setShowModal(true)}
          selects={[
            {
              key: "status",
              label: "All statuses",
              options: uniqueStatuses
            },
            {
              key: "manager",
              label: "All managers",
              options: uniqueManagers
            }
          ]}
        />

        <div className="requests-list">
          {filtered.map((req) => (
            <RequestCard key={req.id} {...req} />
          ))}
        </div>

        {showModal && (
          <NewRequestModal
            onClose={() => setShowModal(false)}
            onSuccess={async () => {
              await loadRequests();
              setShowModal(false);
            }}
          />
        )}
      </div>
    </div>
  );
};

export default RequestsPage;