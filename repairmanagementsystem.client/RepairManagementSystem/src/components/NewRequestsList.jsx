import React from "react";
import NewRequestCard from "./NewRequestCard";
import "./styles/NewRequestsList.css";

const NewRequestsList = ({ newRequests, onAssign }) => (
    <div className="request-wrapper">
        <div className="request-cards">
            {newRequests.map((r) => (
                <NewRequestCard
                    key={r.repairRequestId}
                    repairRequestId={r.repairRequestId}
                    name={r.name}
                    repairObjectType={r.type}
                    createdAt={r.createdAt}
                    description={r.description}
                    managerId={r.managerId}
                    onAssign={onAssign}
                />
            ))}
        </div>
    </div>
);

export default NewRequestsList;