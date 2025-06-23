import React from "react";
import { useState } from "react";
import { useNavigate } from "react-router-dom";
import "./styles/WorkerCard.css";

const statusColors = {
    Available: "#4ade80",
    Unavailable: "#f87171",
};

const activityStatusColors = {
    OPEN: "#4ade80",
    CANCELED: "#f87171",
    "IN PROGRESS": "#fbbf24",
    "TO DO": "#4ade00",
    COMPLETED: "#22c55e",
    CLOSED: "#f87171",
};

const WorkerCard = ({ id, name, email, status, expertise, activities = [] }) => {
    const key = status;
    const badgeColor = statusColors[key] || "#9ca3af";
    const [showActivities, setShowActivities] = useState(false);

    const navigate = useNavigate();
    const handleActivityClick = (repairRequestId) => {
        navigate(`/manage-request/${repairRequestId}`);
    };

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
            <button
                className="toggle-activities-button"
                onClick={() => setShowActivities(v => !v)}
            >
                {showActivities ? "Hide activities" : "Show activities"}
            </button>

            {showActivities && (
                <ul className="worker-activities-list">
                    {activities.length > 0 ? (
                        activities.map(act => (
                            <li key={act.repairActivityId} className="worker-activity-item"
                                onClick={() => handleActivityClick(act.repairRequestId)}>
                                <span className="activity-seq">{act.sequenceNumber}.</span>{" "}
                                <span className="activity-name">{act.name}</span>{" "}
                                <span
                                    className="activity-status-badge"
                                    style={{ backgroundColor: activityStatusColors[act.status] || "#ccc" }}
                                >
                                    {act.status}
                                </span>
                            </li>
                        ))
                    ) : (
                        <li className="no-activities">No assigned activities</li>
                    )}
                </ul>
            )}
        </div>
    );
};

export default WorkerCard;
