import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/TaskCard.css";
import StatusBadge from "./StatusBadge";

const statusColors = {
    "to do": "gray",
    "in progress": "yellow",
    "closed": "red",
    "completed": "green"
};

const formatDate = (dateStr) => {
    const date = new Date(dateStr);
    if (dateStr === "0001-01-01T00:00:00" || isNaN(date)) return "Brak daty";
    return date.toLocaleString("pl-PL", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    });
};

const TaskCard = ({ id, name, status, createdAt, index }) => {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(`/tasks/my/${index}`);
    };

    return (
        <div className="task-card" onClick={handleClick}>
            <div className="task-name">{name}</div>
            <StatusBadge status={status} />
            <div className="task-date">{formatDate(createdAt)}</div>
        </div>
    );
};

export default TaskCard;
