import React from "react";
import "./styles/StatusBadge.css";

const statusColors = {
    "to do": "gray",
    "in progress": "yellow",
    "cancelled": "red",
    "completed": "green",
    "closed": "red",
    "blue": "blue"
};

const StatusBadge = ({ status, floating }) => {
    const colorClass = statusColors[status.toLowerCase()] || "gray";
    return (
        <div className={`status-badge ${colorClass} ${floating ? "floating" : ""}`}>
            {status}
        </div>
    );
};

export default StatusBadge;
