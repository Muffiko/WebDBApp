import React, { useEffect, useState } from "react";
import "./styles/NewRequestModal.css";
import { useRepairObjectApi } from "../api/repairObjects";
import { useRepairRequestApi } from "../api/repairRequests";

const NewRequestModal = ({ onClose, onSuccess }) => {
  const { getCustomerRepairObjects } = useRepairObjectApi();
  const { addRepairRequest } = useRepairRequestApi();

  const [repairObjects, setRepairObjects] = useState([]);
  const [repairObjectId, setRepairObjectId] = useState("");
  const [description, setDescription] = useState("");
  const [error, setError] = useState("");

  useEffect(() => {
    const loadObjects = async () => {
      try {
        const data = await getCustomerRepairObjects();
        setRepairObjects(data);
      } catch {
        setError("Failed to load repair objects.");
      }
    };

    loadObjects();
  }, []);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!repairObjectId || !description) return;

    try {
      await addRepairRequest({ repairObjectId: parseInt(repairObjectId), description });
      onSuccess();
    } catch (err) {
      setError(err?.message || "Failed to create request.");
    }
  };

  return (
    <div className="modal-overlay">
      <form className="modal-card" onSubmit={handleSubmit}>
        <div className="modal-header">
          <button type="button" onClick={onClose} className="modal-back">←</button>
          <h2>New repair</h2>
        </div>

        <label>Repair object:</label>
        <select
          value={repairObjectId}
          onChange={(e) => setRepairObjectId(e.target.value)}
          required
        >
          <option value="">Select...</option>
          {repairObjects.map((obj) => (
            <option key={obj.repairObjectId} value={obj.repairObjectId}>
              {obj.name} ({obj.repairObjectType.name})
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

        {error && <p className="form-message error">{error}</p>}
        <button type="submit">Create</button>
      </form>
    </div>
  );
};

export default NewRequestModal;