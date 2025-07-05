import React, { useState, useEffect, useRef } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import ActivitiesList from "../components/ActivitiesList";
import NewActivityModal from "../components/NewActivityModal";
import { useRepairRequestApi } from "../api/repairRequests";
import { useRepairActivityApi } from "../api/repairActivity";
import { useUserApi } from "../api/user";
import { useWorkersApi } from "../api/worker";
import RequestStatusModal from "../components/RequestStatusModal";
import "./styles/ManageRequestPage.css";

const ManageRequestPage = () => {
  const navigate = useNavigate();
  const [showModal, setShowModal] = useState(false);
  const { addRepairActivity, updateRepairActivity, updateWorkerRepairActivity } = useRepairActivityApi();
  const { id } = useParams();
  const { getRepairRequestById, changeRepairRequestStatus } = useRepairRequestApi();
  const [requests, setRequests] = useState({});
  const [activities, setActivities] = useState([]);
  const { getUsers } = useUserApi();
  const { updateWorkerAvailability } = useWorkersApi();
  const [showStatusModal, setShowStatusModal] = useState(false);
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

      const r = await getRepairRequestById(id);
      const newAct = r.repairActivities.find(a =>
        a.sequenceNumber === nextSeq && a.name === formData.name
      );
      const newActivity = {
        repairRequestId: created.repairRequestId,
        sequenceNumber: created.sequenceNumber,
        repairActivityId: created.repairActivityId,
        repairActivityTypeId: created.repairActivityTypeId,
        name: created.name,
        activityType: created.repairActivityTypeId,
        description: created.description,
        workerId: created.workerId,
        status: created.status,

        createdAt: new Date(created.createdAt).toLocaleDateString("en-GB"),
        startedAt: formData.startedAt
          ? new Date(formData.startedAt).toLocaleDateString("en-GB")
          : "",
        finishedAt: created.finishedAt
          ? new Date(created.finishedAt).toLocaleDateString("en-GB")
          : "",
      };

      if (formData.workerId) {
        await updateWorkerRepairActivity(newAct.repairActivityId, formData.workerId);
        // await updateWorkerAvailability(formData.workerId, false);
      }

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
      const oldWorkerId = current.workerId;
      const newWorkerId = changes.workerId ?? oldWorkerId;
      const payload = {
        repairActivityTypeId: changes.repairActivityTypeId,
        name: changes.name,
        sequenceNumber: current.sequenceNumber,
        description: changes.description,
        repairRequestId: id,
        result: changes.result,
        status: changes.status,
        startedAt: changes.startedAt,
        finishedAt: changes.finishedAt,
      };
      const updated = await updateRepairActivity(repairActivityId, payload);

      if (oldWorkerId !== newWorkerId) {
        if (newWorkerId) {
          await updateWorkerRepairActivity(repairActivityId, newWorkerId);
        }
      }

      if (oldWorkerId) {
        // await updateWorkerAvailability(oldWorkerId, true);
      }
      if (newWorkerId) {
        // await updateWorkerAvailability(newWorkerId, false);
      }

      setActivities(prev =>
        prev.map(a =>
          a.repairActivityId === repairActivityId
            ? {
              ...a,
              name: updated.name,
              sequenceNumber: updated.sequenceNumber,
              description: updated.description,
              repairRequestId: updated.repairRequestId,
              workerId: newWorkerId,
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
        started: new Date(r.createdAt).toLocaleDateString(),
        finished: r.finishedAt
          ? new Date(r.finishedAt).toLocaleDateString()
          : "Not finished",
        status: r.status,
        description: r.description,
        result: r.result || "Not finished"
      })

      const formattedActivities = (r.repairActivities || []).map(a => ({
        repairActivityId: a.repairActivityId,
        sequenceNumber: a.sequenceNumber,
        name: a.name,
        activityType: a.repairActivityType.repairActivityTypeId,
        description: a.description || "",
        workerId: a.workerId,
        result: a.result || "",
        status: a.status
          .toUpperCase(),
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
    OPEN: "blue",
    "IN PROGRESS": "yellow",
    CLOSED: "gray",
    CANCELLED: "red",
    COMPLETED: "green",
    "TO DO": "blue",
  }[requests.status];

  const isReadOnly = ["COMPLETED", "CANCELLED"].includes(requests.status);

  const handleChangeRequestResult = async (repairRequestId, newStatus, result) => {
    try {
      await changeRepairRequestStatus(repairRequestId, newStatus, result);
      await loadRepairRequests();
    } catch (err) {
      console.error("Error updating request result:", err);
    }
  };

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
                <p>
                  <strong>Result:</strong> {requests.result || "N/A"}
                </p>
              </div>
            </div>
          </div>

          <div className="request-section">
            <h3>
              Description:
              {requests.status !== "COMPLETED" && requests.status !== "CANCELLED" && (
                <button
                  className="btn-status"
                  onClick={() => setShowStatusModal(true)}
                >
                  Change Status/Result
                </button>
              )}
            </h3>
            <p className="request-description">{requests.description}</p>
          </div>

          <ActivitiesList
            activities={activities}
            onAdd={!isReadOnly ? () => setShowModal(true) : undefined}
            onUpdate={handleUpdateActivity}
            isReadOnly={isReadOnly}
          />

          {showModal && (
            <NewActivityModal
              onClose={() => setShowModal(false)}
              onSubmit={handleAddActivity}
              nextSeq={nextSeq}
            />
          )}

          {showStatusModal && (
            <RequestStatusModal
              currentStatus={requests.status}
              hasActivities={activities.some(a => ["TO DO", "IN PROGRESS"].includes(a.status?.toUpperCase()))}
              onClose={() => setShowStatusModal(false)}
              onSubmit={(newStatus, newResult) =>
                handleChangeRequestResult(requests.id, newStatus, newResult)
              }
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
