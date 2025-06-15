import React, { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import ActivitiesList from "../components/ActivitiesList";
import NewActivityModal from "../components/NewActivityModal";
import { useRepairRequestApi } from "../api/repairRequests";
import { useUserApi } from "../api/user";
import "./styles/ManageRequestPage.css";

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
  const [requests, setRequests] = useState({});
  const [activities, setActivities] = useState([]);
  const { getUsers } = useUserApi();

  const loadRepairRequests = async () => {
    try {
      const users = await getUsers();
      const map = Object.fromEntries(
        users.map(u => [u.userId, `${u.firstName} ${u.lastName}`])
      );
      console.log("All users from API:", users);
      const r = await getRepairRequestById(id);
      console.log("Loaded repair request data:", r);

      const customerName = map[r.repairObject.customerId] || `#${r.repairObject.customerId}`;
      const managerName = r.managerId
        ? (map[r.managerId] || `#${r.managerId}`)
        : "Not assigned";

      // const workerName = r.workerId
      //   ? (map[r.workerId] || `#${r.workerId}`)
      //   : "Not assigned";

      setRequests({
        id: r.repairRequestId,
        name: r.repairObject.name,
        type: r.repairObject.repairObjectType.name,
        customer: customerName,
        manager: managerName,
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

      const formattedActivities = (r.repairActivities || []).map(a => ({
        repairActivityId: a.repairActivityId,
        name: a.repairActivityType.name,
        activityType: a.repairActivityType.repairActivityTypeId,
        description: a.description || "",
        worker: map[a.workerId] || `#${a.workerId}`,
        status: a.status,
        createdAt: new Date(a.createdAt).toLocaleDateString("en-GB"),
        startedAt: a.startedAt
          ? new Date(a.startedAt).toLocaleDateString("en-GB")
          : "",
        finishedAt: a.finishedAt
          ? new Date(a.finishedAt).toLocaleDateString("en-GB")
          : "",
      }));
      setActivities(formattedActivities);
    } catch (err) {
      console.error("Failed to load requests or activities:", err);
    }
  };

  useEffect(() => {
    loadRepairRequests();
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
