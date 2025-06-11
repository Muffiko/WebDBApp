import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import NewRequestsList from "../components/NewRequestsList";
import "./styles/NewRequestsPage.css";

const initialRequests = [
  {
    id: 1,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: "Jan Kowalski",
  },
  {
    id: 2,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 3,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 4,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 5,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 6,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 7,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: "Jan Kowalski",
  },
  {
    id: 8,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 9,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 10,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
  {
    id: 11,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
  },
];

const NewRequestsPage = () => {
  const [filters, setFilters] = useState({ name: "", type: "" });
  const [requests, setRequests] = useState(initialRequests);

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
              options: ["Elektroniczne", "AGD", "Inne"],
            },
          ]}
        />
        <NewRequestsList newRequests={filtered} onAssign={handleAssign} />
      </main>
    </div>
  );
};

export default NewRequestsPage;