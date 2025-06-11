import React, { useState, useRef, useEffect } from "react";
import "./styles/NewRequestCard.css";

const allManagers = [
    "Jan Kowalski",
    "Marcin Kowalski",
    "Kamil Kowalski",
    "Anna Wiśniewska",
];

const NewRequestCard = ({ id, name, type, createdAt, description, manager: initialManager , onAssign}) => {
    const [isExpanded, setIsExpanded] = useState(false);
    const [manager, setManager] = useState(initialManager);
    const selectRef = useRef();

    const handleAssign = (e) => {
        e.stopPropagation();
        onAssign(id, manager);
        setIsExpanded(false);
    };

    return (
        <div
            className={`request-card ${isExpanded ? "expanded" : ""}`}
            onClick={() => setIsExpanded((v) => !v)}
        >
            <div className="request-badge"> {id} </div>
            <span className="expand-icon">
                {isExpanded ? "▾" : "▸"}
            </span>
            <div className="request-header">
                <div className="request-name">{name}</div>
                <div className="request-type">{type}</div>
                <div className="request-date">{createdAt}</div>
                <div className="request-manager">
                    {isExpanded ? (
                        <select
                            ref={selectRef}
                            className="manager-select"
                            value={manager}
                            onChange={e => setManager(e.target.value)}
                            onClick={e => e.stopPropagation()}
                        >
                            <option value="">Assign manager…</option>
                            {allManagers.map(m => (
                                <option key={m} value={m}>
                                    {m}
                                </option>
                            ))}
                        </select>
                    ) : (
                        <span className="manager-label">
                            {manager || "—"}
                        </span>
                    )}
                </div>
            </div>

            {isExpanded && (
                <div className="request-details">
                    <div className="request-description"> {description} </div>
                </div>
            )}

            {manager && isExpanded && (
                <button
                    className="assign-button"
                    onClick={handleAssign}
                >
                    Assign
                </button>
            )}

        </div>
    );
};

export default NewRequestCard;
