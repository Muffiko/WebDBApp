import React, { useState, useRef, useEffect } from "react";
import "./styles/NewRequestCard.css";
import { useManagerApi } from "../api/manager";

const NewRequestCard = ({ repairRequestId, name, repairObjectType, createdAt, description, managerId: initialManagerId, onAssign }) => {
    const [isExpanded, setIsExpanded] = useState(false);
    const [managerId, setManagerId] = useState(initialManagerId);
    const selectRef = useRef();

    const { getManagers } = useManagerApi();
    const [managers, setManagers] = useState([]);

    const loadManagers = async () => {
        try {
            const data = await getManagers();
            const mapped = data.map(m => ({
                managerId: m.managerId,
                firstName: m.user.firstName,
                lastName: m.user.lastName,
            }));
            setManagers(mapped);
        } catch (error) {
            console.error("Error loading managers:", error);
        }
    };

    useEffect(() => {
        loadManagers();
    }, []);

    const handleSelectChange = e => {
        e.stopPropagation();
        const newId = Number(e.target.value);
        setManagerId(newId);
    };

    const displayManager = () => {
        const m = managers.find(m => m.managerId === managerId);
        return m ? `${m.firstName} ${m.lastName}` : managerId || "Unassigned";
    };


    return (
        <div
            className={`new-request-card ${isExpanded ? "expanded" : ""}`}
            onClick={() => setIsExpanded((v) => !v)}
        >
            <div className="new-request-badge"> {repairRequestId} </div>
            <span className="new-expand-icon">
                {isExpanded ? "▾" : "▸"}
            </span>
            <div className="new-request-header">
                <div className="new-request-name">{name}</div>
                <div className="new-request-repairObjectType">{repairObjectType}</div>
                <div className="new-request-date">{createdAt}</div>
                <div className="new-request-manager">
                    {isExpanded ? (
                        <select
                            ref={selectRef}
                            className="new-manager-select"
                            value={managerId}
                            onChange={handleSelectChange}
                            onClick={e => e.stopPropagation()}
                        >
                            <option value="">Assign manager…</option>
                            {managers.map(m => (
                                <option key={m.managerId} value={m.managerId}>
                                    {m.firstName} {m.lastName}
                                </option>
                            ))}
                        </select>
                    ) : (
                        <span className="new-manager-label">
                            {displayManager()}
                        </span>
                    )}
                </div>
            </div>

            {isExpanded && (
                <div className="new-request-details">
                    <div className="new-request-description"> {description} </div>
                </div>
            )}

            {managerId && isExpanded && (
                <button
                    className="new-assign-button"
                    onClick={e => {
                        e.stopPropagation();
                        onAssign(repairRequestId, managerId);

                        setIsExpanded(false);
                    }}
                >
                    Assign
                </button>
            )}

        </div>
    );
};

export default NewRequestCard;