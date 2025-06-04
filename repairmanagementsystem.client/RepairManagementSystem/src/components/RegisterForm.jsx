import React, { useState, useContext } from "react";
import "./styles/RegisterForm.css";
import { registerUser } from "../api/auth";
import { AuthContext } from "../contexts/AuthContext";
import { useNavigate } from "react-router-dom";

const RegisterForm = () => {
  const [formData, setFormData] = useState({
    name: "",
    surname: "",
    email: "",
    phoneNumber: "",
    country: "",
    city: "",
    postalCode: "",
    street: "",
    houseNumber: "",
    apartment: "",
    password: "",
    confirmPassword: ""
  });

  const [errors, setErrors] = useState({});
  const [globalError, setGlobalError] = useState(null);
  const [success, setSuccess] = useState(null);
  const [loading, setLoading] = useState(false);
  const { login } = useContext(AuthContext);
  const navigate = useNavigate();

  const handleChange = (e) => {
    setFormData((prev) => ({
      ...prev,
      [e.target.name]: e.target.value
    }));
    setErrors((prev) => ({ ...prev, [e.target.name]: null }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setErrors({});
    setGlobalError(null);
    setSuccess(null);

    if (formData.password !== formData.confirmPassword) {
      setErrors({ confirmPassword: "Passwords do not match." });
      return;
    }

    if (formData.password.length < 8) {
      setErrors({ password: "Password must be at least 8 characters." });
      return;
    }

    setLoading(true);
    try {
      const response = await registerUser(formData);
      const data = await response.json();

      if (!response.ok) {
        if (data.errors) {
          const backendErrors = {};
          for (const key in data.errors) {
            const simplifiedKey = key.startsWith("Address.")
              ? key.split(".")[1].toLowerCase()
              : key.toLowerCase();
            backendErrors[simplifiedKey] = data.errors[key][0];
          }
          setErrors(backendErrors);
        } else {
          setGlobalError(data.title || "Registration failed.");
        }
        return;
      }

      setSuccess("Account created successfully.");

      console.log(data);

      login(data.token);

      if (response.role === "Manager") navigate("/new-requests");
      else navigate("/profile");

    } catch (err) {
      setGlobalError("Unexpected error occurred.");
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="register-card">
      <h1 className="register-title">RMS</h1>
      <p className="register-subtitle">Register</p>
      <form onSubmit={handleSubmit} className="register-form">
        <div className="input-group-row">
          <div className="input-wrapper">
            <label>Name</label>
            <input name="name" placeholder="Enter your name" value={formData.name} onChange={handleChange} />
            {errors.name && <small className="error">{errors.name}</small>}
          </div>
          <div className="input-wrapper">
            <label>Surname</label>
            <input name="surname" placeholder="Enter your surname" value={formData.surname} onChange={handleChange} />
            {errors.surname && <small className="error">{errors.surname}</small>}
          </div>
        </div>

        <div className="input-wrapper">
          <label>Email</label>
          <input name="email" type="email" placeholder="e.g. example@domain.com" value={formData.email} onChange={handleChange} />
          {errors.email && <small className="error">{errors.email}</small>}
        </div>

        <div className="input-wrapper">
          <label>Phone number</label>
          <input name="phoneNumber" placeholder="e.g. +48 123 456 789" value={formData.phoneNumber} onChange={handleChange} />
          {errors.phonenumber && <small className="error">{errors.phonenumber}</small>}
        </div>

        <div className="input-wrapper">
          <label>Country</label>
          <input name="country" placeholder="Enter your country" value={formData.country} onChange={handleChange} />
          {errors.country && <small className="error">{errors.country}</small>}
        </div>

        <div className="input-wrapper">
          <label>City</label>
          <input name="city" placeholder="Enter your city" value={formData.city} onChange={handleChange} />
          {errors.city && <small className="error">{errors.city}</small>}
        </div>

        <div className="input-wrapper">
          <label>Postal Code</label>
          <input name="postalCode" placeholder="e.g. 00-000" value={formData.postalCode} onChange={handleChange} />
          {errors.postalcode && <small className="error">{errors.postalcode}</small>}
        </div>

        <div className="input-wrapper">
          <label>Street</label>
          <input name="street" placeholder="Enter your street" value={formData.street} onChange={handleChange} />
          {errors.street && <small className="error">{errors.street}</small>}
        </div>

        <div className="input-group-row">
          <div className="input-wrapper">
            <label>House Number</label>
            <input name="houseNumber" placeholder="e.g. 12A" value={formData.houseNumber} onChange={handleChange} />
            {errors.housenumber && <small className="error">{errors.housenumber}</small>}
          </div>
          <div className="input-wrapper">
            <label>Apartment</label>
            <input name="apartment" placeholder="e.g. 5" value={formData.apartment} onChange={handleChange} />
            {errors.apartnumber && <small className="error">{errors.apartnumber}</small>}
          </div>
        </div>

        <div className="input-wrapper">
          <label>Password</label>
          <input name="password" type="password" placeholder="At least 8 characters" value={formData.password} onChange={handleChange} />
          {errors.password && <small className="error">{errors.password}</small>}
        </div>

        <div className="input-wrapper">
          <label>Confirm Password</label>
          <input name="confirmPassword" type="password" placeholder="Repeat your password" value={formData.confirmPassword} onChange={handleChange} />
          {errors.confirmPassword && <small className="error">{errors.confirmPassword}</small>}
        </div>

        {globalError && <p className="error">{globalError}</p>}
        {success && <p style={{ color: "lightgreen" }}>{success}</p>}

        <button type="submit" className="register-button" disabled={loading}>
          {loading ? "Creating..." : "Create account"}
        </button>
      </form>
    </div>
  );
};

export default RegisterForm;
