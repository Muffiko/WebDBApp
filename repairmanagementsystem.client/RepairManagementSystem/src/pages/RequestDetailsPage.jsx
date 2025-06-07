import React from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import ActivitiesList from "../components/ActivitiesList";
import "./styles/RequestDetailsPage.css";

const managerMenuItems = [
  { path: "/new-requests", label: "New Requests", icon: "🟦" },
  { path: "/open-requests", label: "Open Requests", icon: "📂" },
  { path: "/workers", label: "Workers", icon: "🗂️" },
  { path: "/profile", label: "Profile", icon: "👤" },
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
    "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text...",
};

const mockActivities = [
  {
    id: 1,
    name: "Diagnostyka",
    worker: "Mariusz Kowalski",
    status: "Completed",
  },
  {
    id: 2,
    name: "Czyszczenie wnętrza",
    worker: "Mariusz Kowalski",
    status: "Canceled",
    description: "Lorem Ipsum",
    activityType: "Cleaning",
    createdAt: "01/01/2025",
    startedAt: "02/01/2025",
    finishedAt: "03/01/2025",
  },
  {
    id: 3,
    name: "Wymiana dysku",
    worker: "Mariusz Kowalski",
    status: "In progress",
  },
  {
    id: 4,
    name: "Aktualizacja BIOS",
    worker: "Mariusz Kowalski",
    status: "To do",
  },
  { id: 5, name: "Test RAM", worker: "Mariusz Kowalski", status: "To do" },
];

const RequestDetailsPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();

  const data = mockData;

  const statusColor = {
    Open: "blue",
    "In progress": "yellow",
    Closed: "gray",
  }[data.status];

  const handleAddActivity = () => {
    alert("TODO");
  };

  return (
    <div className="request-container">
      <Sidebar menuItems={managerMenuItems} />
      <div className="request-page">
        <h1 className="request-title">Request Details</h1>

        <div className="request-card">
          <div className={`request-status-label status--${statusColor}`}>
            {data.status}
          </div>

          <div className="request-section">
            <h3>Details:</h3>
            <div className="request-grid">
              <div>
                <p>
                  <strong>Name:</strong> {data.name}
                </p>
                <p>
                  <strong>Object type:</strong> {data.type}
                </p>
                <p>
                  <strong>Customer:</strong> {data.customer}
                </p>
              </div>
              <div>
                <p>
                  <strong>Manager:</strong> {data.manager}
                </p>
                <p>
                  <strong>Created:</strong> {data.created}
                </p>
                <p>
                  <strong>Started:</strong> {data.started}
                </p>
                <p>
                  <strong>Finished:</strong> {data.finished || "N/A"}
                </p>
              </div>
            </div>
          </div>

          <div className="request-section">
            <h3>Description:</h3>
            <p className="request-description">{data.description}</p>
          </div>

          <ActivitiesList
            activities={mockActivities}
            onAdd={handleAddActivity}
          />

          <button className="request-back" onClick={() => navigate(-1)}>
            ← Go back
          </button>
        </div>
      </div>
    </div>
  );
};

export default RequestDetailsPage;
