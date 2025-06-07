import React, { useState } from "react";
import { useRef } from "react";
import "./styles/ActivityCard.css";
const statusColors = {
  completed: "#4ade80",
  canceled: "#f87171",
  "in progress": "#fbbf24",
  todo: "#9ca3af",
};

const ActivityCard = ({ id, name: initialName, worker: initialWorker, status, description: initialDescription, activityType: initialActivityType, createdAt, startedAt, finishedAt }) => {
  const key = status.toLowerCase();
  const badgeColor = statusColors[key] || statusColors.todo;
  const [isExpanded, setIsExpanded] = useState(false);
  const [name, setName] = useState(initialName);
  const [worker, setWorker] = useState(initialWorker);
  const [activityType, setActivityType] = useState(initialActivityType);
  const [description, setDescription] = useState(initialDescription);

  const [editField, setEditField] = useState(null);
  const inputRef = useRef(null);

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
                  ✏️
                </span>
              )}
            </>
          )}
        </div>
        <div className="activity-worker">
          <span className="field-label">Worker:</span>
          {editField === "worker" ? (
            <input
              ref={inputRef}
              className="inline-input"
              value={worker}
              onChange={(e) => setWorker(e.target.value)}
              onBlur={finishEdit}
              onKeyDown={onKey}
            />
          ) : (
            <>
              <span className="field-value">{worker}</span>
              {isExpanded && (
                <span
                  className="edit-icon"
                  onClick={(e) => {
                    e.stopPropagation();
                    startEdit("worker");
                  }}
                >
                  ✏️
                </span>
              )}
            </>
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
              {editField === "activityType" ? (
                <input
                  ref={inputRef}
                  className="inline-input"
                  value={activityType}
                  onChange={(e) => setActivityType(e.target.value)}
                  onBlur={finishEdit}
                  onKeyDown={onKey}
                />
              ) : (
                <>
                  <span className="field-value">{activityType}</span>
                  <span
                    className="edit-icon"
                    onClick={(e) => {
                      e.stopPropagation();
                      startEdit("activityType");
                    }}
                  >
                    ✏️
                  </span>
                </>
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
                  ✏️
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
