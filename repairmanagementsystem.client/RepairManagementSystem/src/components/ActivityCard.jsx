import React, { useState, useEffect } from "react";
import { useRef } from "react";
import "./styles/ActivityCard.css";
import { useWorkersApi } from "../api/worker";
import { useRepairActivityTypesApi } from "../api/repairActivityType";

const statusColors = {
  Open: "#4ade80",
  Canceled: "#f87171",
  "In Progress": "#fbbf24",
  Todo: "#9ca3af",
};

const ActivityCard = ({ id, name: initialName, activityType: initialActivityType, description: initialDescription, worker: initialWorker, status, createdAt, startedAt, finishedAt }) => {
  const badgeColor = statusColors[status] || statusColors.Todo;
  const [isExpanded, setIsExpanded] = useState(false);
  const [name, setName] = useState(initialName);
  const [worker, setWorker] = useState(initialWorker);
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
      const onlyWorkers = data.filter(w => w.role === "Worker");
      const mapped = onlyWorkers.map((w, wdx) => ({
        id: wdx + 1,
        workerId: w.id,
        name: `${w.firstName} ${w.lastName}`,
        email: w.email,
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

  const workersOptions = initialWorker
    ? Array.from(new Set([initialWorker, ...workers.map(w => w.name)]))
    : workers.map(w => w.name);

  const activitiesOptions = initialActivityType
    ? Array.from(new Set([initialActivityType, ...activityTypes.map(a => a.repairActivityTypeId)]))
    : activityTypes.map(a => a.repairActivityTypeId);

  const startEdit = (field) => {
    setEditField(field);
    setIsExpanded(true);
    setTimeout(() => inputRef.current && inputRef.current.focus(), 0);
  };

  const handleToggle = () => {
    if (editField) return;
    setIsExpanded(x => !x);
  };

  const finishEdit = () => setEditField(null);

  const onKey = (e) => {
    if (e.key === "Enter") finishEdit();
  };
  return (

    <div
      className={`activity-card ${isExpanded ? "expanded" : ""}`}
      onClick={handleToggle}
    >
      <div
        className="activity-badge"
        style={{ backgroundColor: badgeColor }}
      >
        {id}
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
            />
          ) : (
            <>
              <span className="field-value">{name}</span>
              {isExpanded && (
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
              value={worker}
              onChange={e => setWorker(e.target.value)}
              onClick={e => e.stopPropagation()}
            >
              <option value="">Assign worker…</option>
              {workersOptions.map(w => (
                <option key={w} value={w}>
                  {w}
                </option>
              ))}
            </select>
          ) : (
            <span className="field-value">{worker}</span>
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
                  onChange={e => setActivityType(e.target.value)}
                  onClick={e => e.stopPropagation()}
                >
                  <option value="">Assign activity</option>
                  {activitiesOptions.map(typeId => (
                    <option key={typeId} value={typeId}>
                      {typeId}
                    </option>
                  ))}
                </select>
              ) : (
                <span className="field-value">{activityType}</span>
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
              />
            ) : (
              <>
                <span
                  className="edit-icon"
                  onClick={(e) => {
                    e.stopPropagation();
                    startEdit("description");
                  }}
                >
                  ✎
                </span>
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
