import react, { useState, useEffect } from "react";
import "./styles/NewActivityModal.css";
import { useWorkersApi } from "../api/worker";
import { useRepairActivityTypesApi } from "../api/repairActivityType";

const NewActivityModal = ({ onClose, onSubmit, nextSeq }) => {
    const [activityName, setActivityName] = useState("");
    const [activityType, setActivityType] = useState("");
    const [workerId, setWorkerId] = useState();
    const [description, setDescription] = useState("");
    const { getWorkers } = useWorkersApi();
    const [workers, setWorkers] = useState([]);

    const loadWorkers = async () => {
        try {
            const data = await getWorkers();
            const mapped = data.map((w, wdx) => ({
                id: wdx + 1,
                workerId: w.workerId,
                name: `${w.user.firstName} ${w.user.lastName}`,
                email: w.user.email,
                isAvailable: w.isAvailable,
            }));
            setWorkers(mapped);
        } catch (error) {
            console.error("Error loading workers:", error);
        }
    };

    const { getRepairActivityTypes } = useRepairActivityTypesApi();
    const [activityTypes, setActivityTypes] = useState([]);

    const loadActivityTypes = async () => {
        try {
            const data = await getRepairActivityTypes();
            setActivityTypes(data);
        } catch (error) {
            console.error("Error loading activity types:", error);
        }
    };

    useEffect(() => {
        loadWorkers();
        loadActivityTypes();
    }, []);

    const handleSubmit = async (e) => {
        e.preventDefault();

        const payload = {
            repairActivityTypeId: activityType,
            name: activityName,
            sequenceNumber: nextSeq,
            description,
            workerId: workerId,
            status: "TO DO",
        };
        await onSubmit(payload);
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
                    <option value="" disabled>Select...</option>
                    {activityTypes.map((type) => (
                        <option key={type.repairActivityTypeId} value={type.repairActivityTypeId}>
                            {type.name}
                        </option>
                    ))}
                </select>

                <label>Worker:</label>
                <select value={workerId || ""} onChange={(e) => setWorkerId(Number(e.target.value))}
                >
                    <option value="" disabled>Select...</option>
                    {workers.map((w) => (
                        <option
                            key={w.workerId}
                            value={w.workerId}
                            disabled={!w.isAvailable}
                            style={{ color: w.isAvailable ? 'inherit' : '#ccc' }}
                        >
                            {w.name}
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