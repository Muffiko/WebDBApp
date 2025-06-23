import React, { useState } from "react";
import "./styles/RequestStatusModal.css";

const RequestStatusModal = ({ currentStatus, currentResult, hasActivities, onClose, onSubmit }) => {
    const [result, setResult] = useState(currentResult || "");

    const statusOptions = ["COMPLETED", "CANCELLED"].filter(
        (s) => s !== currentStatus
    );

    const [status, setStatus] = useState("");

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit(status, result);
        onClose();
    };

    return (
        <div className="modal-overlay">
            <form className="modal-card" onSubmit={handleSubmit}>
                <div className="modal-header">
                    <button type="button" onClick={onClose} className="modal-back">←</button>
                    <h2>Change Status / Result</h2>
                </div>

                {hasActivities && (
                    <p className="modal-warning">
                        Status cannot be changed as long as there are activities.
                    </p>
                )}
                <label>Status:</label>
                <select
                    value={status}
                    onChange={(e) => setStatus(e.target.value)}
                    required
                    disabled={hasActivities}
                >
                    <option value="" disabled>
                        Choose status
                    </option>
                    {statusOptions.map((s) => (
                        <option key={s} value={s}>
                            {s}
                        </option>
                    ))}
                </select>

                <label>Result:</label>
                <textarea
                    value={result}
                    onChange={(e) => setResult(e.target.value)}
                    rows={4}
                    required
                    disabled={hasActivities}
                />

                <button type="submit" disabled={hasActivities}>Save</button>
            </form>
        </div>
    );
};

export default RequestStatusModal;
