import React, { useState } from "react";
import "./styles/NewRepairModal.css";

const NewRepairModal = ({ onClose, onSubmit }) => {
  const [formData, setFormData] = useState({
    name: "",
    type: "",
    description: ""
  });

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    onSubmit(formData);
    onClose();
  };

  return (
    <div className="modal-overlay">
      <div className="modal-content">
        <button className="modal-back" onClick={onClose}>←</button>
        <h2 className="modal-title">New repair</h2>
        <form className="modal-form" onSubmit={handleSubmit}>
          <label>Name:</label>
          <input
            type="text"
            name="name"
            value={formData.name}
            onChange={handleChange}
          />

          <label>Type:</label>
          <select
            name="type"
            value={formData.type}
            onChange={handleChange}
          >
            <option value="">Select...</option>
            <option value="Laptop">Laptop</option>
            <option value="Phone">Phone</option>
            <option value="Monitor">Monitor</option>
          </select>

          <label>Description:</label>
          <textarea
            name="description"
            rows="4"
            value={formData.description}
            onChange={handleChange}
          />

          <button type="submit" className="modal-submit">Create</button>
        </form>
      </div>
    </div>
  );
};

export default NewRepairModal;
