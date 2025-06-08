import React, { useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import ActivitiesList from "../components/ActivitiesList";
import NewActivityModal from "../components/NewActivityModal";
import "./styles/RequestDetailsPage.css";

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
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);
  const [activities, setActivities] = useState(mockActivities);

  const handleAddActivity = (formData) => {
    const nextId = activities.length
      ? Math.max(...activities.map(a => a.id)) + 1
      : 1

    const newActivity = {
      id: nextId,
      name: formData.activityName,
      worker: formData.worker ? formData.worker : "To be assigned",
      status: "To do",
      activityType: formData.activityType,
      description: formData.description,
      createdAt: new Date().toLocaleDateString("en-GB"),
      startedAt: "",
      finishedAt: ""
    };

    setActivities(prev => [...prev, newActivity]);
    setShowModal(false);

    console.log("New activity added:", newActivity);
  };

  const statusColor = {
    Open: "blue",
    "In progress": "yellow",
    Closed: "gray",
  }[mockData.status];

  return (
    <div className="request-container">
      <Sidebar />
      <div className="request-page">
        <h1 className="request-title">Request Details</h1>

        <div className="request-card">
          <div className={`request-status-label status--${statusColor}`}>
            {mockData.status}
          </div>

          <div className="request-section">
            <h3>Details:</h3>
            <div className="request-grid">
              <div>
                <p>
                  <strong>Name:</strong> {mockData.name}
                </p>
                <p>
                  <strong>Object type:</strong> {mockData.type}
                </p>
                <p>
                  <strong>Customer:</strong> {mockData.customer}
                </p>
              </div>
              <div>
                <p>
                  <strong>Manager:</strong> {mockData.manager}
                </p>
                <p>
                  <strong>Created:</strong> {mockData.created}
                </p>
                <p>
                  <strong>Started:</strong> {mockData.started}
                </p>
                <p>
                  <strong>Finished:</strong> {mockData.finished || "N/A"}
                </p>
              </div>
            </div>
          </div>

          <div className="request-section">
            <h3>Description:</h3>
            <p className="request-description">{mockData.description}</p>
          </div>

          <ActivitiesList
            activities={activities}
            onAdd={() => setShowModal(true)}
          />

          {showModal && (
            <NewActivityModal
              onClose={() => setShowModal(false)}
              onSubmit={handleAddActivity}
            />
          )}

          <button className="request-back" onClick={() => navigate(-1)}>
            ← Go back
          </button>
        </div>
      </div>
    </div>
  );
};

export default RequestDetailsPage;