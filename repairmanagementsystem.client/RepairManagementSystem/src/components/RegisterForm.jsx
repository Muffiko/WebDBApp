// src/components/RegisterForm.jsx
import React, { useState } from "react";
import "./styles/RegisterForm.css";

const RegisterForm = () => {
  const [formData, setFormData] = useState({
    name: "",
    surname: "",
    email: "",
    phone: "",
    country: "",
    city: "",
    postalCode: "",
    street: "",
    houseNumber: "",
    apartment: "",
    password: "",
    confirmPassword: ""
  });

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
  };

  const handleSubmit = (e) => {
    e.preventDefault();
    console.log("Creating account:", formData);
  };

  return (
    <div className="register-card">
      <h1 className="register-title">RMS</h1>
      <p className="register-subtitle">Register</p>
      <form onSubmit={handleSubmit} className="register-form">
        <div className="input-group-row">
          <input type="text" name="name" placeholder="Name" value={formData.name} onChange={handleChange} />
          <input type="text" name="surname" placeholder="Surname" value={formData.surname} onChange={handleChange} />
        </div>
        <input type="email" name="email" placeholder="Email" value={formData.email} onChange={handleChange} />
        <input type="text" name="phone" placeholder="Phone number" value={formData.phone} onChange={handleChange} />
        <input type="text" name="country" placeholder="Country" value={formData.country} onChange={handleChange} />
        <input type="text" name="city" placeholder="City" value={formData.city} onChange={handleChange} />
        <input type="text" name="postalCode" placeholder="Postal Code" value={formData.postalCode} onChange={handleChange} />
        <input type="text" name="street" placeholder="Street" value={formData.street} onChange={handleChange} />
        <div className="input-group-row">
          <input type="text" name="houseNumber" placeholder="House Number" value={formData.houseNumber} onChange={handleChange} />
          <input type="text" name="apartment" placeholder="Apartment" value={formData.apartment} onChange={handleChange} />
        </div>
        <input type="password" name="password" placeholder="Password" value={formData.password} onChange={handleChange} />
        <input type="password" name="confirmPassword" placeholder="Confirm password" value={formData.confirmPassword} onChange={handleChange} />
        <button type="submit" className="register-button">Create account</button>
      </form>
    </div>
  );
};

export default RegisterForm;
