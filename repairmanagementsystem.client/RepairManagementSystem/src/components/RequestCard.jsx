import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/RequestCard.css";

const statusColors = {
  OPEN: "blue",
  "IN PROGRESS": "yellow",
  CLOSED: "gray",
  COMPLETED: "green",
  CANCELLED: "red",
  "TO DO": "#4ade00"
};

const RequestCard = ({ id, name, status, managerId, dateCreated }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/requests/${id}`);
  };

  return (
    <div className="request-card" onClick={handleClick}>
      <div className="request-col request-name">{name}</div>
      <div className="request-col">
        <div className={`request-status ${statusColors[status]}`}>
          {status}
        </div>
      </div>
      <div className="request-col request-manager">{managerId}</div>
      <div className="request-col request-date">{dateCreated}</div>
    </div>
  );
};

export default RequestCard;
