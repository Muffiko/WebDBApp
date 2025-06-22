import React from "react";
import { useNavigate } from "react-router-dom";
import "./styles/TaskCard.css";

const statusColors = {
    OPEN: "blue",
    IN_PROGRESS: "yellow",
    CLOSED: "gray"
};

const TaskCard = ({ id, name, status, dateCreated }) => {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate(`/requests/${id}`);
    };

    return (
        <div className="task-card" onClick={handleClick}>
            <div className="task-name">{name}</div>
            <div className={`task-status ${statusColors[status.toLowerCase()]}`}>
                {status}
            </div>
            <div className="task-date">{dateCreated}</div>
        </div>
    );
};

export default TaskCard;
