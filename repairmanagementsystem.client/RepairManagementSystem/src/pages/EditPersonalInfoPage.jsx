import React, { useState } from "react";
import "./styles/EditForms.css";
import { useUserApi } from "../api/user";
import { useNavigate } from "react-router-dom";

const EditPersonalInfoPage = () => {
  const { updateUserInfo } = useUserApi();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
  });

  const [errors, setErrors] = useState({});
  const [message, setMessage] = useState("");

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setErrors({});

    try {
      await updateUserInfo(form);
      navigate("/profile");
    } catch (err) {
      if (err.errors) {
        const mapped = {};
        for (const key in err.errors) {
          mapped[key.charAt(0).toLowerCase() + key.slice(1)] = err.errors[key][0];
        }
        setErrors(mapped);
      } else {
        setMessage("Something went wrong.");
      }
    }
  };

  return (
    <div className="form-page">
      <form className="form-card" onSubmit={handleSubmit}>
        <h2>Edit personal info</h2>

        <div className="row">
          <div className="input-wrapper">
            <label>Name</label>
            <input
              type="text"
              name="firstName"
              value={form.firstName}
              onChange={handleChange}
            />
            {errors.firstName && <small className="error">{errors.firstName}</small>}
          </div>

          <div className="input-wrapper">
            <label>Surname</label>
            <input
              type="text"
              name="lastName"
              value={form.lastName}
              onChange={handleChange}
            />
            {errors.lastName && <small className="error">{errors.lastName}</small>}
          </div>
        </div>

        <div className="input-wrapper">
          <label>Email</label>
          <input
            type="email"
            name="email"
            value={form.email}
            onChange={handleChange}
          />
          {errors.email && <small className="error">{errors.email}</small>}
        </div>

        <div className="input-wrapper">
          <label>Phone number</label>
          <input
            type="text"
            name="phoneNumber"
            value={form.phoneNumber}
            onChange={handleChange}
          />
          {errors.phoneNumber && <small className="error">{errors.phoneNumber}</small>}
        </div>

        <button type="submit">Update</button>
        {message && <p className="form-message">{message}</p>}
      </form>
    </div>
  );
};

export default EditPersonalInfoPage;
