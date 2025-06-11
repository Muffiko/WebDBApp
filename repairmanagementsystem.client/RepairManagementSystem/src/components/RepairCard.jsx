import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/RepairCard.css";

const statusColors = {
  open: "#4ade80",
  "in progress": "#facc15",
  closed: "#f87171",
};

const RepairCard = ({ id, name, status, manager, dateCreated }) => {
  const navigate = useNavigate();
  const key = status.toLowerCase();
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
        
        <div className="repair-date">{dateCreated}</div>
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
