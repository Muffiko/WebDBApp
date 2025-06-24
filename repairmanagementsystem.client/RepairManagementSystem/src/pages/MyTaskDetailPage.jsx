import React, { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import "./styles/MyTaskDetailPage.css";
import { useRepairActivityApi } from "../api/repairActivity";

const statusColors = {
    "TO DO": "gray",
    "IN PROGRESS": "yellow",
    "CANCELLED": "red",
    "COMPLETED": "green"
};

const MyTaskDetailPage = () => {
    const { number } = useParams();
    const { getMyRepairActivities } = useRepairActivityApi();
    const [activity, setActivity] = useState(null);
    const [allActivities, setAllActivities] = useState([]);
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchActivities = async () => {
            try {
                const data = await getMyRepairActivities();
                setAllActivities(data);
                const index = parseInt(number, 10) - 1;
                if (index < 0 || index >= data.length) {
                    throw new Error("Invalid task number.");
                }
                setActivity(data[index]);
            } catch (err) {
                setError(err.message || "Error loading activity");
            }
        };

        fetchActivities();
    }, [getMyRepairActivities, number]);

    if (error) {
        return <div style={{ color: "red" }}>{error}</div>;
    }

    if (!activity) {
        return <p>Loading...</p>;
    }

    const {
        name,
        repairActivityType,
        sequenceNumber,
        description,
        status,
        result,
        workerId,
        request = {}
    } = activity;

    return (
        <div className="task-detail-container">
            <Sidebar />
            <div className="task-detail-page">
                <h1>Request</h1>
                <div className="request-card">
                    <div className="request-header">
                        <div>
                            <h3>Details:</h3>
                            <p><strong>Name:</strong> {request.name || name}</p>
                            <p><strong>Object type:</strong> {request.objectType || "Elektroniczne"}</p>
                            <p><strong>Customer:</strong> {request.customer || "Przemysław Kowalski"}</p>
                        </div>
                        <div>
                            <p><strong>Manager:</strong> {request.manager || "Kamil Kowalski"}</p>
                            <p><strong>Created:</strong> {request.createdAt || "01/01/2025"}</p>
                            <p><strong>Started:</strong> {request.startedAt || "05/01/2025"}</p>
                            <p><strong>Finished:</strong> {request.finishedAt || ""}</p>
                        </div>
                        <div className={`status-badge ${statusColors[status]}`}>
                            {status}
                        </div>
                    </div>

                    <div className="description">
                        <h3>Description:</h3>
                        <p>{description || "No description provided."}</p>
                    </div>

                    <div className="task-sections">
                        <h3>Your active tasks on this request:</h3>
                        <div className="task-card">
                            <div className={`seq seq-${sequenceNumber}`}>{sequenceNumber}</div>
                            <div className="task-body">
                                <p><strong>Name:</strong> {name}</p>
                                <p><strong>Worker:</strong> Mariusz Kowalski</p>
                            </div>
                            <div className={`status-button ${statusColors[status]}`}>{status}</div>
                        </div>
                    </div>

                    {["CANCELLED", "COMPLETED"].includes(status) && (
                        <div className="task-result">
                            <h3>Result:</h3>
                            <p>{result || "No result provided."}</p>
                        </div>
                    )}
                </div>
            </div>
        </div>
    );
};

export default MyTaskDetailPage;