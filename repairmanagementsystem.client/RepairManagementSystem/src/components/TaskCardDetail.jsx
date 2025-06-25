import React, { useState, useEffect } from "react";
import "./styles/TaskCardDetail.css";
import { formatDate, formatStatus, statusColors } from "../utils";
import { useRepairActivityApi } from "../api/repairActivity";

const TaskCardDetail = ({ task, expanded, onToggle, onChangeStatus }) => {
    const { changeRepairActivityStatus } = useRepairActivityApi();

    const [modalOpen, setModalOpen] = useState(false);
    const [newStatus, setNewStatus] = useState(task.status || "");
    const [result, setResult] = useState("");
    const [errorMessage, setErrorMessage] = useState("");
    const [successMessage, setSuccessMessage] = useState("");

    const taskStatus = String(task.status).toLowerCase();
    const taskStatusClass = statusColors[taskStatus] || "gray";

    useEffect(() => {
        // Disable background scroll when modal is open
        if (modalOpen) {
            document.body.style.overflow = "hidden";
        } else {
            document.body.style.overflow = "auto";
        }

        return () => {
            document.body.style.overflow = "auto";
        };
    }, [modalOpen]);

    const openModal = (e) => {
        e.stopPropagation();
        setNewStatus(task.status || "");
        setResult("");
        setErrorMessage("");
        setSuccessMessage("");
        setModalOpen(true);
    };

    const closeModal = () => {
        setModalOpen(false);
        setErrorMessage("");
        setSuccessMessage("");
    };

    const handleSubmit = async (e) => {
        e.preventDefault();

        if (!newStatus) {
            setErrorMessage("Please select a status");
            return;
        }

        const needsResult = ["CANCELLED", "COMPLETED"].includes(newStatus);
        if (needsResult && !result.trim()) {
            setErrorMessage("Result is required when status is CANCELLED or COMPLETED");
            return;
        }

        setModalOpen(false);
        setErrorMessage("");
        setSuccessMessage("");

        try {
            await changeRepairActivityStatus(task.repairActivityId, {
                status: newStatus,
                result,
            });

            if (onChangeStatus) {
                await onChangeStatus(task.repairActivityId, {
                    status: newStatus,
                    result,
                });
            }

            window.location.reload();
        } catch (error) {
            console.error("Error updating task status:", error);
        }
    };

    return (
        <div
            className="task-card-detail"
            onClick={(e) => {
                if (!modalOpen) onToggle?.(e);
            }}
        >
            <div className={`status-edge ${taskStatusClass}`} />

            <div className="task-card-content">
                {/* LEFT */}
                <div className="task-left">
                    <p className="task-name">{task.name}</p>
                    {expanded && (
                        <p className="task-type">
                            <strong>Activity type:</strong>{" "}
                            {task.repairActivityType?.name || "N/A"}
                        </p>
                    )}
                </div>

                {/* RIGHT */}
                <div className="task-right">
                    {expanded && (
                        <>
                            <p>
                                <strong>Created at:</strong>{" "}
                                {formatDate(task.createdAt)}
                            </p>
                            <p>
                                <strong>Result:</strong>{" "}
                                {task.result || "No result yet"}
                            </p>
                        </>
                    )}

                    <span className={`task-status-detail ${taskStatusClass}`}>
                        {formatStatus(task.status)}
                    </span>

                    {expanded && (
                        <button
                            className="change-status-button"
                            onClick={openModal}
                            m
                        >
                            Change Status
                        </button>
                    )}
                </div>
            </div>

            {/* DESCRIPTION */}
            {expanded && (
                <div className="task-description-full">
                    <p>
                        <strong>Description:</strong>
                    </p>
                    <p>{task.description || "No description provided."}</p>
                </div>
            )}

            {/* MODAL */}
            {modalOpen && (
                <div
                    className="result-popup-overlay"
                    onClick={closeModal}
                    role="dialog"
                    aria-modal="true"
                >
                    <div
                        className="result-popup"
                        onClick={(e) => e.stopPropagation()}
                        tabIndex={-1}
                    >
                        <h3 style={{ marginTop: "0px" }}>Change Statuss</h3>
                        <form onSubmit={handleSubmit}>
                            <label>
                                <select
                                    value={newStatus}
                                    onChange={(e) =>
                                        setNewStatus(e.target.value)
                                    }
                                    required
                                    style={{
                                        width: "100%",
                                        backgroundColor: "#2e2e2e",
                                        color: "#eee",
                                        border: "1px solid #444",
                                        borderRadius: "8px",
                                        padding: "8px",
                                    }}
                                >
                                    <option value="">Select status</option>
                                    <option value="TO DO">TO DO</option>
                                    <option value="IN PROGRESS">
                                        IN PROGRESS
                                    </option>
                                    <option value="COMPLETED">COMPLETED</option>
                                    <option value="CANCELLED">CANCELLED</option>
                                </select>
                            </label>

                            {(newStatus === "CANCELLED" ||
                                newStatus === "COMPLETED") && (
                                <label
                                    style={{
                                        display: "block",
                                        marginTop: "10px",
                                    }}
                                >
                                    <textarea
                                        value={result}
                                        onChange={(e) =>
                                            setResult(e.target.value)
                                          }
                                        required
                                    />
                                </label>
                            )}

                            {errorMessage && (
                                <p className="error-message">{errorMessage}</p>
                            )}
                            {successMessage && (
                                <p className="success-message">
                                    {successMessage}
                                </p>
                            )}

                            <div className="popup-buttons">
                                <button
                                    type="button"
                                    onClick={closeModal}
                                    className="change-status-button cancel"
                                >
                                    Cancel
                                </button>
                                <button
                                    type="submit"
                                    className="change-status-button"
                                >
                                    Save
                                </button>
                            </div>
                        </form>
                    </div>
                </div>
            )}
        </div>
    );
};

export default TaskCardDetail;
