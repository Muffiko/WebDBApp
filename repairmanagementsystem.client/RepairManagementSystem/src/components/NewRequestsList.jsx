import React from "react";
import NewRequestCard from "./NewRequestCard";
import "./styles/NewRequestsList.css";

const NewRequestsList = ({ newRequests, onAssign }) => (
    <div className="request-wrapper">
        <div className="request-cards">
            {newRequests.map((r, index) => (
                <NewRequestCard
                    key={r.id}
                    id={index + 1}
                    name={r.name}
                    type={r.type}
                    createdAt={r.createdAt}
                    description={r.description}
                    manager={r.manager}
                    onAssign={onAssign}
                />
            ))}
        </div>
    </div>
);

export default NewRequestsList;