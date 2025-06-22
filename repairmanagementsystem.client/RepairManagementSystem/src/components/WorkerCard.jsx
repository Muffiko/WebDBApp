import React from "react";
import "./styles/WorkerCard.css";

const statusColors = {
    Available: "#4ade80",
    Unavailable: "#f87171",
};

const WorkerCard = ({ id, name, email, status, expertise }) => {
    const key = status;
    const badgeColor = statusColors[key] || "#9ca3af";

    return (
        <div className="worker-card">
            <div
                className="worker-badge"
                style={{ backgroundColor: badgeColor }}
            >
                {id}
            </div>

            <div className="worker-header">
                <div className="worker-name">{name}</div>
                <div className="worker-email">{email}</div>
                <div className="worker-expertise">{expertise}</div>

                <div
                    className="worker-status"
                    style={{ backgroundColor: badgeColor }}
                >
                    {status}
                </div>
            </div>
        </div>
    );
};

export default WorkerCard;
