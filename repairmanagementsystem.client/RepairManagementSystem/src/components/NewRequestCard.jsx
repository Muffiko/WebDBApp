import React, { useState, useRef, useEffect } from "react";
import "./styles/NewRequestCard.css";
import { useManagerApi } from "../api/manager";

const NewRequestCard = ({ id, name, repairObjectType, createdAt, description, manager: initialManager, onAssign }) => {
    const [isExpanded, setIsExpanded] = useState(false);
    const [manager, setManager] = useState(initialManager || "");
    const selectRef = useRef();

    const { getManagers } = useManagerApi();
    const [managers, setManagers] = useState([]);

    const loadManagers = async () => {
        try {
            const data = await getManagers();
            const onlyManagers = data.filter(m => m.role === "Manager");
            const mapped = onlyManagers.map((m, mdx) => ({
                id: mdx + 1,
                managerId: m.id,
                name: `${m.firstName} ${m.lastName}`,
                email: m.email,
            }));
            setManagers(mapped);
        } catch (error) {
            console.error("Error loading managers:", error);
        }
    };

    useEffect(() => {
        loadManagers();
    }, []);

    const managersOptions = initialManager
        ? Array.from(new Set([initialManager, ...managers.map(m => m.name)]))
        : managers.map(m => m.name);

    const handleAssign = (e) => {
        e.stopPropagation();
        onAssign(id, manager);
        setIsExpanded(false);
    };

    return (
        <div
            className={`new-request-card ${isExpanded ? "expanded" : ""}`}
            onClick={() => setIsExpanded((v) => !v)}
        >
            <div className="new-request-badge"> {id} </div>
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
                            value={manager}
                            onChange={e => setManager(e.target.value)}
                            onClick={e => e.stopPropagation()}
                        >
                            <option value="">Assign manager…</option>
                            {managersOptions.map(m => (
                                <option key={m} value={m}>
                                    {m}
                                </option>
                            ))}
                        </select>
                    ) : (
                        <span className="new-manager-label">
                            {manager || "—"}
                        </span>
                    )}
                </div>
            </div>

            {isExpanded && (
                <div className="new-request-details">
                    <div className="new-request-description"> {description} </div>
                </div>
            )}

            {manager && isExpanded && (
                <button
                    className="new-assign-button"
                    onClick={handleAssign}
                >
                    Assign
                </button>
            )}

        </div>
    );
};

export default NewRequestCard;