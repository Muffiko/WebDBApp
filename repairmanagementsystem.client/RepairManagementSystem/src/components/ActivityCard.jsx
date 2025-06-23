import React, { useState, useEffect } from "react";
import { useRef } from "react";
import "./styles/ActivityCard.css";
import { useWorkersApi } from "../api/worker";
import { useRepairActivityTypesApi } from "../api/repairActivityType";

const statusColors = {
  OPEN: "#4ade80",
  CANCELED: "#f87171",
  "IN PROGRESS": "#fbbf24",
  "TO DO": "#4ade00",
  COMPLETED: "#22c55e",
  CLOSED: "#f87171",
};

const ActivityCard = ({ id, sequenceNumber, name: initialName, activityType: initialActivityType, description: initialDescription, workerId: initialWorkerId, status, createdAt, startedAt, finishedAt, onUpdate, readOnly = false }) => {
  const badgeColor = statusColors[status];
  const [isExpanded, setIsExpanded] = useState(false);
  const [name, setName] = useState(initialName);
  const [workerId, setWorkerId] = useState(initialWorkerId);
  const [activityType, setActivityType] = useState(initialActivityType || "");
  const [description, setDescription] = useState(initialDescription);
  const [editField, setEditField] = useState(null);
  const inputRef = useRef(null);
  const selectRef = useRef();

  const { getWorkers } = useWorkersApi();
  const [workers, setWorkers] = useState([]);

  const loadWorkers = async () => {
    try {
      const data = await getWorkers();
      const mapped = data.map(w => ({
        workerId: w.workerId,
        firstName: w.user.firstName,
        lastName: w.user.lastName,
        email: w.user.email,
        isAvailable: w.isAvailable
      }));
      setWorkers(mapped);
    } catch (error) {
      console.error("Error loading workers:", error);
    }
  };

  const { getRepairActivityTypes } = useRepairActivityTypesApi();
  const [activityTypes, setActivityTypes] = useState([]);

  const loadActivityTypes = async () => {
    try {
      const data = await getRepairActivityTypes();
      setActivityTypes(data);
    } catch (error) {
      console.error("Error loading activity types:", error);
    }
  };

  useEffect(() => {
    loadWorkers();
    loadActivityTypes();
  }, []);

  const startEdit = (field) => {
    setEditField(field);
    setIsExpanded(true);
    setTimeout(() => inputRef.current && inputRef.current.focus(), 0);
  };

  const handleToggle = () => {
    if (editField) return
    setIsExpanded(x => !x);
  };

  const finishEdit = () => {
    if (readOnly) return;
    if (editField === "name") {
      onUpdate(id, { name });
    }
    if (editField === "description") {
      onUpdate(id, { description });
    }
    setEditField(null);
  };

  const onKey = (e) => {
    if (e.key === "Enter") finishEdit();
  };

  const handleWorkerChange = e => {
    if (readOnly) return;
    const newId = Number(e.target.value);
    setWorkerId(newId);
    onUpdate(id, { workerId: newId, status: "TO DO" });
  };

  const displayWorker = () => {
    const w = workers.find(w => w.workerId === workerId);
    return w ? `${w.firstName} ${w.lastName}` : workerId || "Unassigned";
  };

  const handleTypeChange = e => {
    if (readOnly) return;
    const newType = e.target.value;
    setActivityType(newType);
    onUpdate(id, { repairActivityTypeId: newType });
  };

  return (

    <div
      className={`activity-card ${isExpanded ? "expanded" : ""}`}
      onClick={handleToggle}
      style={{ cursor: readOnly ? "default" : "pointer" }}
    >
      <div
        className="activity-badge"
        style={{ backgroundColor: badgeColor }}
      >
        {sequenceNumber}
      </div>
      <div className="activity-header">

        <div className="activity-name">
          <span className="field-label">Name:</span>
          {editField === "name" ? (
            <input
              ref={inputRef}
              className="inline-input"
              value={name}
              onChange={(e) => setName(e.target.value)}
              onBlur={finishEdit}
              onKeyDown={onKey}
              disabled={readOnly}
            />
          ) : (
            <>
              <span className="field-value">{name}</span>
              {isExpanded && !readOnly && (
                <span
                  className="edit-icon"
                  onClick={(e) => {
                    e.stopPropagation();
                    startEdit("name");
                  }}
                >
                  ✎
                </span>
              )}
            </>
          )}
        </div>
        <div className="activity-worker">
          <div className="field-label">Worker:</div>
          {isExpanded ? (
            <select
              ref={selectRef}
              className="worker-select"
              value={workerId}
              onChange={handleWorkerChange}
              onClick={e => e.stopPropagation()}
              disabled={readOnly}
            >
              <option value="" disabled>
                Choose worker
              </option>
              {workers.map((w) => (
                <option
                  key={w.workerId}
                  value={w.workerId}
                  disabled={!w.isAvailable}
                  style={{ color: w.isAvailable ? 'inherit' : '#ccc' }}
                >
                  {w.firstName} {w.lastName}
                </option>
              ))}
            </select>
          ) : (
            <span className="field-value">{displayWorker()}</span>
          )}
        </div>
        <div
          className="activity-status"
          style={{ backgroundColor: badgeColor }}
        >
          {status}
          {isExpanded && <span className="status-caret">▾</span>}
        </div>
      </div>

      {isExpanded && (
        <div className="activity-details">
          <div className="details-top">
            <div className="details-left">
              <span className="field-label">Activity type:</span>
              {isExpanded ? (
                <select
                  ref={selectRef}
                  className="activity-select"
                  value={activityType}
                  onChange={handleTypeChange}
                  onClick={e => e.stopPropagation()}
                  disabled={readOnly}
                >
                  <option value="" disabled>
                    Choose activity
                  </option>
                  {activityTypes.map(t => (
                    <option
                      key={t.repairActivityTypeId}
                      value={t.repairActivityTypeId}
                    >
                      {t.name}
                    </option>
                  ))}
                </select>
              ) : (
                <span className="field-value">{(activityTypes.find(t => t.repairActivityTypeId === activityType)
                  || {}
                ).name || activityType}</span>
              )}
            </div>
            <div className="details-right">
              <p>
                <span className="field-label">Created at:</span>{" "}
                <span className="field-value">{createdAt}</span>
              </p>
              <p>
                <span className="field-label">Started at:</span>{" "}
                <span className="field-value">{startedAt}</span>
              </p>
              <p>
                <span className="field-label">Finished at:</span>{" "}
                <span className="field-value">{finishedAt}</span>
              </p>
            </div>
          </div>
          <div className="activity-description">
            <span className="field-label">Description:</span>
            {editField === "description" ? (
              <textarea
                ref={inputRef}
                className="inline-textarea"
                value={description}
                onChange={(e) => setDescription(e.target.value)}
                onBlur={finishEdit}
                onKeyDown={onKey}
                rows={4}
                disabled={readOnly}
              />
            ) : (
              <>
                {!readOnly && (
                  <span
                    className="edit-icon"
                    onClick={(e) => {
                      e.stopPropagation();
                      startEdit("description");
                    }}
                  >
                    ✎
                  </span>
                )}
                <p className="field-value">{description}</p>
              </>
            )}
          </div>
        </div>
      )}
    </div>
  );
};

export default ActivityCard;
