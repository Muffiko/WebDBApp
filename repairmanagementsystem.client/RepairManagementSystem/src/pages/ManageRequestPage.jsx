import React, { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import ActivitiesList from "../components/ActivitiesList";
import NewActivityModal from "../components/NewActivityModal";
import { useRepairRequestApi } from "../api/repairRequests";
import { useRepairActivityApi } from "../api/repairActivity";
import "./styles/ManageRequestPage.css";

// const mockData = {
//   id: 1,
//   name: "Komputer",
//   type: "Elektroniczne",
//   customer: "Przemysław Kowalski",
//   manager: "Kamil Kowalski",
//   created: "01/01/2025",
//   started: "05/01/2025",
//   finished: "",
//   status: "In progress",
//   description:
//     "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text...",
// };

// const mockActivities = [
//   {
//     id: 1,
//     name: "Diagnostyka",
//     worker: "Mariusz Kowalski",
//     status: "Completed",
//   },
//   {
//     id: 2,
//     name: "Czyszczenie wnętrza",
//     worker: "Mariusz Kowalski",
//     status: "Canceled",
//     description: "Lorem Ipsum",
//     activityType: "Cleaning",
//     createdAt: "01/01/2025",
//     startedAt: "02/01/2025",
//     finishedAt: "03/01/2025",
//   },
//   {
//     id: 3,
//     name: "Wymiana dysku",
//     worker: "Mariusz Kowalski",
//     status: "In progress",
//   },
//   {
//     id: 4,
//     name: "Aktualizacja BIOS",
//     worker: "Mariusz Kowalski",
//     status: "To do",
//   },
//   { id: 5, name: "Test RAM", worker: "Mariusz Kowalski", status: "To do" },
// ];

const ManageRequestPage = () => {
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);

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

  const { id } = useParams();
  const { getRepairRequestById } = useRepairRequestApi();
  const [requests, setRequests] = useState([]);

  const loadRepairRequests = async () => {
    try {
      const r = await getRepairRequestById(id);
      console.log("Loaded repair request data:", r);
      setRequests({
        id: r.repairRequestId,
        name: r.repairObject.name,
        type: r.repairObject.repairObjectType.name,
        customer: r.repairObject.customerId,
        manager:
          r.manager && r.manager.firstName
            ? `${r.manager.firstName} ${r.manager.lastName}`
            : 'Not assigned',
        created: new Date(r.createdAt).toLocaleDateString(),
        started: r.startedAt
          ? new Date(r.startedAt).toLocaleDateString()
          : "Not started",
        finished: r.finishedAt
          ? new Date(r.finishedAt).toLocaleDateString()
          : "Not finished",
        status: r.status,
        description: r.description
      })
    } catch (err) {
      console.error("Failed to load requests:", err);
    }
  };

  const { getRepairActivities } = useRepairActivityApi();
  const [activities, setActivities] = useState([]);

  const loadRepairActivities = async () => {
    try {
      const a = await getRepairActivities(id);
      console.log("Loaded repair activities data:", a);
      setActivities({
        id: a.repairActivityId,
        name: a.name,
        type: a.repairActivityType.name,
        worker: a.workerId,
        status: a.status,
        description: a.description || "",
        createdAt: new Date(a.createdAt).toLocaleDateString("en-GB"),
        startedAt: a.startedAt
          ? new Date(a.startedAt).toLocaleDateString("en-GB")
          : "",
      })
    } catch (err) {
      console.error("Failed to load activities:", err);
    }
  }

  useEffect(() => {
    loadRepairRequests();
    loadRepairActivities();
  }, [id]);

  const statusColor = {
    Open: "blue",
    "In progress": "yellow",
    Closed: "gray",
  }[requests.status];

  return (
    <div className="request-container">
      <Sidebar />
      <div className="request-page">
        <h1 className="request-title">Request Details</h1>

        <div className="request-card">
          <div className={`request-status-label status--${statusColor}`}>
            {requests.status}
          </div>

          <div className="request-section">
            <h3>Details:</h3>
            <div className="request-grid">
              <div>
                <p>
                  <strong>Name:</strong> {requests.name}
                </p>
                <p>
                  <strong>Object type:</strong> {requests.type}
                </p>
                <p>
                  <strong>Customer:</strong> {requests.customer}
                </p>
              </div>
              <div>
                <p>
                  <strong>Manager:</strong> {requests.manager}
                </p>
                <p>
                  <strong>Created:</strong> {requests.created}
                </p>
                <p>
                  <strong>Started:</strong> {requests.started}
                </p>
                <p>
                  <strong>Finished:</strong> {requests.finished || "N/A"}
                </p>
              </div>
            </div>
          </div>

          <div className="request-section">
            <h3>Description:</h3>
            <p className="request-description">{requests.description}</p>
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

export default ManageRequestPage;