import React, { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import ActivitiesList from "../components/ActivitiesList";
import NewActivityModal from "../components/NewActivityModal";
import { useRepairRequestApi } from "../api/repairRequests";
import { useRepairActivityApi } from "../api/repairActivity";
import { useUserApi } from "../api/user";
import "./styles/ManageRequestPage.css";

const ManageRequestPage = () => {
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);
  const { addRepairActivity, updateRepairActivity } = useRepairActivityApi();
  const { id } = useParams();
  const { getRepairRequestById } = useRepairRequestApi();
  const [requests, setRequests] = useState({});
  const [activities, setActivities] = useState([]);
  const { getUsers } = useUserApi();

  const nextSeq = activities.reduce(
    (max, act) => Math.max(max, Number(act.sequenceNumber) || 0),
    0
  ) + 1;

  const handleAddActivity = async (formData) => {
    try {
      const payload = {
        repairActivityTypeId: formData.repairActivityTypeId,
        name: formData.name,
        sequenceNumber: nextSeq,
        description: formData.description,
        repairRequestId: id,
        workerId: formData.workerId,
        status: formData.status,
        startedAt: formData.startedAt
      };

      const created = await addRepairActivity(payload);

      const newActivity = {
        repairRequestId: created.repairRequestId,
        sequenceNumber: created.sequenceNumber,
        repairActivityId: created.repairActivityId,
        repairActivityTypeId: created.repairActivityTypeId,
        name: created.name,
        activityType: created.repairActivityTypeId,
        description: created.description,
        workerId: created.workerId,
        status: formData.status,

        createdAt: new Date(created.createdAt).toLocaleDateString("en-GB"),
        startedAt: formData.startedAt
          ? new Date(formData.startedAt).toLocaleDateString("en-GB")
          : "",
        finishedAt: created.finishedAt
          ? new Date(created.finishedAt).toLocaleDateString("en-GB")
          : "",
      };

      setActivities(prev => [...prev, newActivity]);
      await loadRepairRequests();
      setShowModal(false);
    } catch (error) {
      console.error("Error adding new activity:", error);
    }
  };

  const handleUpdateActivity = async (repairActivityId, changes) => {
    try {
      const current = activities.find(a => a.repairActivityId === repairActivityId);
      const payload = {
        repairActivityTypeId: changes.repairActivityTypeId,
        name: changes.name,
        sequenceNumber: current.sequenceNumber,
        description: changes.description,
        repairRequestId: id,
        workerId: changes.workerId,
        result: changes.result,
        status: changes.status,
        startedAt: changes.startedAt,
        finishedAt: changes.finishedAt,
      };
      const updated = await updateRepairActivity(repairActivityId, payload);

      setActivities(prev =>
        prev.map(a =>
          a.repairActivityId === repairActivityId
            ? {
              ...a,
              name: updated.name,
              sequenceNumber: updated.sequenceNumber,
              description: updated.description,
              repairRequestId: updated.repairRequestId,
              workerId: updated.workerId,
              status: updated.status,
              result: updated.result,
              startedAt: updated.startedAt
                ? new Date(updated.startedAt).toLocaleDateString("en-GB")
                : "",
              finishedAt: updated.finishedAt
                ? new Date(updated.finishedAt).toLocaleDateString("en-GB")
                : "",
            }
            : a
        )
      );
      await loadRepairRequests();
    } catch (error) {
      console.error("Error updating activity:", error);
    }
  };

  const loadRepairRequests = async () => {
    try {
      const users = await getUsers();
      const map = Object.fromEntries(
        users.map(u => [u.userId, `${u.firstName} ${u.lastName}`])
      );

      const r = await getRepairRequestById(id);

      const customerName = map[r.repairObject.customerId] || `#${r.repairObject.customerId}`;
      const managerName = r.managerId
        ? (map[r.managerId] || `#${r.managerId}`)
        : "Not assigned";

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
        sequenceNumber: a.sequenceNumber,
        name: a.name,
        activityType: a.repairActivityType.repairActivityTypeId,
        description: a.description || "",
        workerId: a.workerId,
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
            onUpdate={handleUpdateActivity}
          />

          {showModal && (
            <NewActivityModal
              onClose={() => setShowModal(false)}
              onSubmit={handleAddActivity}
              nextSeq={nextSeq}
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
