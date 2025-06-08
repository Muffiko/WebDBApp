import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import "./styles/NewRequestsPage.css";

const dummyManagers = ["Kamil Kowalski", "Marcin Nowak", "Anna Wiśniewska"];
const currentUser = "Kamil Kowalski";

const initialRequests = [
  {
    id: 1,
    name: "Komputer",
    type: "Elektroniczne",
    created: "01/01/2025",
    description:
      "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
    manager: null,
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
    id: 3,
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
    id: 3,
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
    id: 3,
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
    id: 3,
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
];

const NewRequestsPage = () => {
  const [filters, setFilters] = useState({ name: "", type: "" });
  const [expandedId, setExpandedId] = useState(null);
  const [editingId, setEditingId] = useState(null);
  const [managerInput, setManagerInput] = useState("");
  const [error, setError] = useState("");
  const [requests, setRequests] = useState(initialRequests);

  const toggleExpand = (id) => {
    setExpandedId((prev) => (prev === id ? null : id));
  };

  const filtered = requests.filter(
    (r) =>
      r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
      (filters.type === "" || r.type === filters.type)
  );

  const handleAssign = (id) => {
    const trimmed = managerInput.trim();
    if (!dummyManagers.includes(trimmed)) {
      setError("Manager does not exist.");
      return;
    }

    setRequests((prev) => prev.filter((r) => r.id !== id));
    setEditingId(null);
    setError("");
  };

  const handleAssignToMe = (id) => {
    setRequests((prev) => prev.filter((r) => r.id !== id));
    setEditingId(null);
    setError("");
  };

  return (
    <div className="requests-container">
      <Sidebar />
      <div className="requests-page">
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

        <div className="request-list">
          {filtered.map((req) => (
            <div
              key={req.id}
              className={`request-card-wide ${
                expandedId === req.id ? "expanded" : ""
              }`}
              onClick={() => toggleExpand(req.id)}
            >
              <div className="req-wide-info">
                <span className="arrow">
                  {expandedId === req.id ? "▲" : "▼"}
                </span>
                <span>{req.name}</span>
                <span>{req.type}</span>
                <span>{req.created}</span>

                {editingId === req.id ? (
                  <div
                    className="assign-edit"
                    onClick={(e) => e.stopPropagation()}
                  >
                    <div className="assign-edit-top">
                      <input
                        placeholder="Search manager..."
                        value={managerInput}
                        onChange={(e) => setManagerInput(e.target.value)}
                      />
                      <button
                        className="confirm"
                        onClick={(e) => {
                          e.stopPropagation();
                          handleAssign(req.id);
                        }}
                      >
                        ✔
                      </button>
                      <button
                        className="cancel"
                        onClick={(e) => {
                          e.stopPropagation();
                          setEditingId(null);
                          setError("");
                        }}
                      >
                        ✖
                      </button>
                    </div>
                    <button
                      className="assign-to-me"
                      onClick={(e) => {
                        e.stopPropagation();
                        handleAssignToMe(req.id);
                      }}
                    >
                      Assign to me
                    </button>
                    {error && (
                      <div style={{ color: "red", fontSize: "0.85rem" }}>
                        {error}
                      </div>
                    )}
                  </div>
                ) : (
                  <button
                    className="assign-button"
                    onClick={(e) => {
                      e.stopPropagation();
                      setEditingId(req.id);
                      setManagerInput("");
                      setError("");
                    }}
                  >
                    Assign manager
                  </button>
                )}
              </div>

              <div className="request-details">
                <strong>Description:</strong>
                <p>{req.description}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
};

export default NewRequestsPage;