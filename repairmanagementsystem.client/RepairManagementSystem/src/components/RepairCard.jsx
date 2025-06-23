import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/RepairCard.css";

const statusColors = {
  OPEN: "#4ade80",
  "IN PROGRESS": "#facc15",
  CLOSED: "#f87171",
  COMPLETED: "#22c55e",
  CANCELLED: "#f87171",
  "TO DO": "#4ade00"
};

const RepairCard = ({ id, name, status, manager, createdAt }) => {
  const navigate = useNavigate();
  const key = status.toUpperCase();
  const badgeColor = statusColors[key];
  const handleClick = () => {
    navigate(`/manage-request/${id}`);
  };

  return (
    <div className="repair-card" onClick={handleClick}
    >
      <div
        className="repair-badge"
        style={{ backgroundColor: badgeColor }}
      >
        {id}
      </div>

      <div className="repair-header">
        <div className="repair-name">{name}</div>

        <div className="repair-manager">{manager}</div>

        <div className="repair-date">{createdAt}</div>
        <div
          className="repair-status"
          style={{ backgroundColor: badgeColor }}
        >
          {status}
        </div>
      </div>
    </div>
  );
};

export default RepairCard;
