import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/RepairCard.css";

const statusColors = {
  open: "blue",
  "in progress": "yellow",
  closed: "gray",
};

const RepairCard = ({ id, name, status, manager, dateCreated }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/repairs/${id}`);
  };

  return (
    <div className="repair-card" onClick={handleClick}>
      <div className="repair-name">{name}</div>
      <div className={`repair-status ${statusColors[status.toLowerCase()]}`}>
        {status}
      </div>
      <div className="repair-manager">{manager}</div>
      <div className="repair-date">{dateCreated}</div>
    </div>
  );
};

export default RepairCard;
