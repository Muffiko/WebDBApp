import React, { useState } from "react";
import "./styles/AddRepairObjectModal.css";

const AddRepairObjectModal = ({ onClose, onSubmit }) => {
  const [formData, setFormData] = useState({
    name: "",
    type: ""
  });

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    if (!formData.name || !formData.type) return;
    onSubmit(formData);
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

        <select
          name="type"
          value={formData.type}
          onChange={handleChange}
          required
        >
          <option value="">Select type...</option>
          <option value="Laptop">Laptop</option>
          <option value="Phone">Phone</option>
          <option value="Other">Other</option>
        </select>

        <button type="submit">Add</button>
      </form>
    </div>
  );
};

export default AddRepairObjectModal;
