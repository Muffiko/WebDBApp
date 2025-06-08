import react, { useState } from "react";
import "./styles/NewActivityModal.css";

const NewActivityModal = ({ onClose, onSubmit }) => {
    const [activityName, setActivityName] = useState("");
    const [activityType, setActivityType] = useState("");
    const [worker, setWorker] = useState("");
    const [description, setDescription] = useState("");

    const activityTypes = [
        { id: "1", name: "Cleaning" },
        { id: "2", name: "Repair" },
        { id: "3", name: "Testing" }
    ];

    const workers = [
        { id: "1", name: "Mariusz Kowalski" },
        { id: "2", name: "Jan Nowak" },
        { id: "3", name: "Anna Wiśniewska" }
    ];

    const handleSubmit = (e) => {
        e.preventDefault();
        onSubmit({
            activityName,
            activityType,
            worker,
            description
        });
        onClose();
    };

    return (
        <div className="modal-overlay">
            <form className="modal-card" onSubmit={handleSubmit}>
                <div className="modal-header">
                    <button type="button" onClick={onClose} className="modal-back">←</button>
                    <h2>New activity</h2>
                </div>

                <label>Activity name:</label>
                <input
                    type="text"
                    value={activityName}
                    onChange={(e) => setActivityName(e.target.value)}
                    required
                />

                <label>Activity type:</label>
                <select value={activityType} onChange={(e) => setActivityType(e.target.value)} required>
                    <option value="">Select...</option>
                    {activityTypes.map((type) => (
                        <option key={type.id} value={type.name}>
                            {type.name}
                        </option>
                    ))}
                </select>

                <label>Worker:</label>
                <select value={worker} onChange={(e) => setWorker(e.target.value)}>
                    <option value="">Select...</option>
                    {workers.map((worker) => (
                        <option key={worker.id} value={worker.name}>
                            {worker.name}
                        </option>
                    ))}
                </select>

                <label>Description:</label>
                <textarea
                    value={description}
                    onChange={(e) => setDescription(e.target.value)}
                    rows={4}
                    required
                />

                <button type="submit">Create</button>
            </form>
        </div>
    );
};

export default NewActivityModal;