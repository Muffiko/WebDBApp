import React from "react";
import { formatDate, statusColors } from "../utils";

const RequestInfoCard = ({ request, managerName }) => {
    const {
        repairObject,
        createdAt,
        startedAt,
        finishedAt,
        description,
        status
    } = request;

    const statusClass = statusColors[status?.toLowerCase()] || "gray";

    return (
        <div className="request-card">
            <div className={`status-edge ${statusClass}`} />
            <div className="request-header">
                <div className="request-info-group">
                    <p><strong>Name:</strong> {repairObject?.name || "Unknown"}</p>
                    <p><strong>Object type:</strong> {repairObject?.repairObjectType?.name || "Unknown"}</p>
                </div>
                <div className="request-info-group">
                    <p><strong>Manager:</strong> {managerName}</p>
                    <p><strong>Created:</strong> {formatDate(createdAt)}</p>
                    <p><strong>Started:</strong> {formatDate(startedAt)}</p>
                    <p><strong>Finished:</strong> {formatDate(finishedAt)}</p>
                </div>
            </div>
            <div className="description">
                <h3>Description:</h3>
                <p>{description || "No description provided."}</p>
            </div>
        </div>
    );
};

export default RequestInfoCard;
