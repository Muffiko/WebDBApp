import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/RequestCard.css";

const statusColors = {
  open: "blue",
  "in progress": "yellow",
  closed: "gray"
};

const RequestCard = ({ id, name, status, manager, dateCreated }) => {
  const navigate = useNavigate();

  const handleClick = () => {
    navigate(`/requests/${id}`);
  };

  return (
    <div className="request-card" onClick={handleClick}>
      <div className="request-col request-name">{name}</div>
      <div className="request-col">
        <div className={`request-status ${statusColors[status.toLowerCase()]}`}>
          {status}
        </div>
      </div>
      <div className="request-col request-manager">{manager}</div>
      <div className="request-col request-date">{dateCreated}</div>
    </div>
  );
};

export default RequestCard;
