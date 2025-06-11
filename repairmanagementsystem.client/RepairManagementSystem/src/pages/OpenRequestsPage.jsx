import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import RepairsList from "../components/RepairsList";
import "./styles/OpenRequestsPage.css";

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
    id: 4,
    name: "Komputer",
    status: "Closed",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 5,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 6,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 7,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 8,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 9,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 10,
    name: "Komputer",
    status: "In progress",
    manager: "Kamil Kowalski",
    dateCreated: "01/01/2025",
  },
  {
    id: 11,
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

  const [repairs] = useState(mockRequests);
  const filtered = repairs.filter(
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
              options: ["Jan Kowalski", "Marcin Kowalski", "Kamil Kowalski"],
            },
          ]}
        />
        <RepairsList repairs={filtered} />
      </main>
    </div>
  );
};

export default OpenRequestsPage;