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
            const simplifiedKey = key.toLowerCase();
            backendErrors[simplifiedKey] = data.errors[key][0];
          }
          setErrors(backendErrors);
        } else {
          setGlobalError(data.title || "Registration failed.");
        }
        return;
      }

      setSuccess("Account created successfully.");
      login(data.token);

      if (response.role === "Manager") navigate("/new-requests");
      else navigate("/profile");

    } catch (err) {
      setGlobalError("Unexpected error occurred.");
    } finally {
      setLoading(false);
    }
  };

  const handleBack = () => {
    navigate("/");
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

          <button type="submit" className="general-button" disabled={loading}>
            {loading ? "Creating..." : "Create account"}
          </button>
        </form>

        <button type="button" onClick={handleBack} className="general-button">
          yBack
        </button>
      </div>
  );
};

export default RegisterForm;
