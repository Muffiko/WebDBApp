import React from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import "./styles/RepairDetailsPage.css";

const menuItems = [
  { path: "/objects", label: "My repair objects", icon: "🧰" },
  { path: "/requests", label: "My requests", icon: "📋" },
  { path: "/profile", label: "Profile", icon: "👤" }
];


const mockData = {
  id: 1,
  name: "Komputer",
  type: "Elektroniczne",
  customer: "Przemysław Kowalski",
  manager: "Kamil Kowalski",
  created: "01/01/2025",
  started: "05/01/2025",
  finished: "",
  status: "In progress",
  description:
    "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text..."
};

const RepairDetailsPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const data = mockData;

  const statusColor = {
    Open: "blue",
    "In progress": "yellow",
    Closed: "gray"
  }[data.status];

  return (
    <div className="repair-container">
      <Sidebar menuItems={menuItems} />
      <div className="repair-page">
        <h1 className="repair-title">Repair</h1>

        <div className="repair-card">
        <div className={`repair-status-label status--${statusColor}`}>
            {data.status}
          </div>

          <div className="repair-section">
            <h3>Details:</h3>
            <div className="repair-grid">
              <div>
                <p><strong>Name:</strong> {data.name}</p>
                <p><strong>Object type:</strong> {data.type}</p>
                <p><strong>Customer:</strong> {data.customer}</p>
              </div>
              <div>
                <p><strong>Manager:</strong> {data.manager}</p>
                <p><strong>Created:</strong> {data.created}</p>
                <p><strong>Started:</strong> {data.started}</p>
                <p><strong>Finished:</strong> {data.finished || "-"}</p>
              </div>
            </div>
          </div>

          <div className="repair-section">
            <h3>Description:</h3>
            <p className="repair-description">{data.description}</p>
          </div>

          <button className="repair-back" onClick={() => navigate(-1)}>
            ← Go back
          </button>
        </div>
      </div>
    </div>
  );
};

export default RepairDetailsPage;
