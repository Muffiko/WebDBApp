import React, { useState } from "react";
import "./styles/NewRepairModal.css";

const NewRepairModal = ({ onClose, onSubmit }) => {
  const [repairObjectId, setRepairObjectId] = useState("");
  const [type, setType] = useState("");
  const [description, setDescription] = useState("");

  const repairObjects = [
    { id: "1", name: "Laptop Lenovo" },
    { id: "2", name: "Komputer stacjonarny" },
    { id: "3", name: "Drukarka HP" }
  ];

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit({
      repairObjectId,
      type,
      description
    });
    onClose();
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
            <option key={obj.id} value={obj.id}>
              {obj.name}
            </option>
          ))}
        </select>

        <label>Type:</label>
        <select value={type} onChange={(e) => setType(e.target.value)} required>
          <option value="">Select...</option>
          <option value="Elektroniczne">Elektroniczne</option>
          <option value="Mechaniczne">Mechaniczne</option>
          <option value="Inne">Inne</option>
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

export default NewRepairModal;
