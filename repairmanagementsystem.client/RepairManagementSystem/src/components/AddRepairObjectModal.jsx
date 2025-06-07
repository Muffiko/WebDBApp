import React, { useState, useEffect } from "react";
import "./styles/AddRepairObjectModal.css";
import { useRepairObjectApi } from "../api/repairObjects";
import { useRepairObjectTypesApi } from "../api/repairObjectTypes";

const AddRepairObjectModal = ({ onClose, onSuccess }) => {
  const { addRepairObject } = useRepairObjectApi();
  const { getRepairObjectTypes } = useRepairObjectTypesApi();

  const [formData, setFormData] = useState({ name: "", type: "" });
  const [types, setTypes] = useState([]);
  const [error, setError] = useState("");

  useEffect(() => {
    const loadTypes = async () => {
      try {
        const data = await getRepairObjectTypes();
        setTypes(data);
      } catch (err) {
        setError("Could not load object types.");
      }
    };
    loadTypes();
  }, []);

  const handleChange = (e) => {
    setFormData(prev => ({ ...prev, [e.target.name]: e.target.value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!formData.name || !formData.type) return;

    try {
      await addRepairObject({
        name: formData.name,
        repairObjectTypeId: formData.type
      });
      //onSuccess(); 
    } catch (err) {
      setError(err?.message || "Failed to add object.");
    }
  };

  return (
    <div className="modal-overlay">
      <form className="modal-card" onSubmit={handleSubmit}>
        <div className="modal-header">
          <button type="button" onClick={onClose} className="modal-back">←</button>
          <h2>Add repair object</h2>
        </div>

        <input
          type="text"
          name="name"
          placeholder="Object name"
          value={formData.name}
          onChange={handleChange}
          required
        />

        <select name="type" value={formData.type} onChange={handleChange} required>
          <option value="">Select type...</option>
          {types.map((type) => (
            <option key={type.repairObjectTypeId} value={type.repairObjectTypeId}>
              {type.repairObjectTypeId.toUpperCase()} - {type.name}
            </option>
          ))}
        </select>

        {error && <p className="form-message error">{error}</p>}
        <button type="submit">Add</button>
      </form>
    </div>
  );
};

export default AddRepairObjectModal;