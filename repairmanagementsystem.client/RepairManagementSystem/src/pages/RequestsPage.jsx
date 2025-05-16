import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import RequestCard from "../components/RequestCard";
import FilterBar from "../components/FilterBar";
import NewRepairModal from "../components/NewRepairModal";
import "./styles/RequestsPage.css";

const menuItems = [
  { path: "/objects", label: "My repair objects", icon: "🧰" },
  { path: "/requests", label: "My requests", icon: "📋" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const mockRequests = [
  {
    id: 1,
    name: "Komputer",
    status: "Open",
    manager: "Jan Kowalski",
    dateCreated: "01/01/2025"
  },
  {
    id: 2,
    name: "Komputer",
    status: "In progress",
    manager: "Marcin Kowalski",
    dateCreated: "01/01/2025"
  },
  {
    id: 3,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025"
  }
];

const RequestsPage = () => {
  const [filters, setFilters] = useState({
    name: "",
    status: "",
    manager: ""
  });

  const [showModal, setShowModal] = useState(false);

  const handleCreate = (formData) => {
    console.log("New repair submitted:", formData);
  };

  const filtered = mockRequests.filter((r) =>
    r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
    (filters.status === "" || r.status === filters.status) &&
    (filters.manager === "" || r.manager === filters.manager)
  );

  return (
    <div className="requests-container">
      <Sidebar menuItems={menuItems} />
      <div className="requests-page">
        <h1 className="requests-title">Repairs</h1>

        <FilterBar
          filters={filters}
          setFilters={setFilters}
          onCreate={() => setShowModal(true)}
        />

        <div className="requests-list">
          {filtered.map((req) => (
            <RequestCard key={req.id} {...req} />
          ))}
        </div>

        {showModal && (
          <NewRepairModal
            onClose={() => setShowModal(false)}
            onSubmit={handleCreate}
          />
        )}
      </div>
    </div>
  );
};

export default RequestsPage;
