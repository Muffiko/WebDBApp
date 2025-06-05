import React from "react";
import "./styles/ActivityCard.css";
//AddRepairObjectModal NewRepairModal
const statusColors = {
  completed: "#4ade80",
  canceled: "#f87171",
  "in progress": "#fbbf24",
  todo: "#9ca3af",
};

const ActivityCard = ({ id, name, worker, status }) => {
  const key = status.toLowerCase();
  const badgeColor = statusColors[key] || statusColors.todo;

  return (
    <div className="activity-card">
      <div className="activity-badge" style={{ backgroundColor: badgeColor }}>
        {id}
      </div>

      <div className="activity-content">
        <div className="activity-field">
          <span className="field-label">Name:</span> {name}
        </div>
        <div className="activity-field">
          <span className="field-label">Worker:</span> {worker}
        </div>
      </div>

      <div className="activity-status" style={{ backgroundColor: badgeColor }}>
        {status}
      </div>
    </div>
  );
};

export default ActivityCard;
