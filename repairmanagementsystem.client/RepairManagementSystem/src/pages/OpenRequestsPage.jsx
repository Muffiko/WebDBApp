// OpenRequestsPage.jsx
import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import RepairCard from "../components/RepairCard";
import NewRepairModal from "../components/NewRepairModal";
import "./styles/OpenRequestsPage.css";

const menuItems = [
  { path: "/new-requests", label: "New Requests", icon: "🟦" },
  { path: "/open-requests", label: "Open Requests", icon: "📂" },
  { path: "/workers", label: "Workers", icon: "🗂️" },
  { path: "/profile", label: "Profile", icon: "👤" },
];

const mockRequests = [
  {
    id: 1,
    name: "Komputer",
    status: "Open",
    manager: "Jan Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 2,
    name: "Komputer",
    status: "In progress",
    manager: "Marcin Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 3,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
];

const OpenRequestsPage = () => {
  const [filters, setFilters] = useState({
    name: "",
    status: "",
    manager: "",
  });

  const [showModal, setShowModal] = useState(false);

  const handleCreate = (formData) => {
    console.log("New repair submitted:", formData);
  };

  const filtered = mockRequests.filter(
    (r) =>
      r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
      (filters.status === "" || r.status === filters.status) &&
      (filters.manager === "" || r.manager === filters.manager)
  );

  return (
    <div className="open-requests-container">
      <Sidebar menuItems={menuItems} />
      <div className="open-requests-page">
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
              options: ["Jan Kowalski", "Marcin Kowalski", "Kamil Kowalski"],
            },
          ]}
        />

        <div className="open-requests-list">
          {filtered.map((req) => (
            <RepairCard key={req.id} {...req} /> 
          ))}
        </div>
        {/* {showModal && (
          <NewRepairModal
            onClose={() => setShowModal(false)}
            onSubmit={handleCreate}
          />
        )} */}
      </div>
    </div>
  );
};

export default OpenRequestsPage;
