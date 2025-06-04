import React, { useState } from "react";
import "./styles/EditForms.css";
import { useUserApi } from "../api/user";
import { useNavigate } from "react-router-dom";

const ChangePasswordPage = () => {
  const { changePassword } = useUserApi();
  const [form, setForm] = useState({
    oldPassword: "",
    newPassword: "",
    confirmNewPassword: "",
  });
  const [message, setMessage] = useState("");
  const [errors, setErrors] = useState({});

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const navigate = useNavigate();
  
  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setErrors({});

    try {
      const msg = await changePassword(form);
      setForm({ oldPassword: "", newPassword: "", confirmNewPassword: "" });
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
        <h2>Change password</h2>

        <div className="input-wrapper">
          <label>Old password</label>
          <input
            type="password"
            name="oldPassword"
            value={form.oldPassword}
            onChange={handleChange}
          />
          {errors.oldPassword && <small className="error">{errors.oldPassword}</small>}
        </div>

        <div className="input-wrapper">
          <label>New password</label>
          <input
            type="password"
            name="newPassword"
            value={form.newPassword}
            onChange={handleChange}
          />
          {errors.newPassword && <small className="error">{errors.newPassword}</small>}
        </div>

        <div className="input-wrapper">
          <label>Confirm new password</label>
          <input
            type="password"
            name="confirmNewPassword"
            value={form.confirmNewPassword}
            onChange={handleChange}
          />
          {errors.confirmNewPassword && <small className="error">{errors.confirmNewPassword}</small>}
        </div>

        <button type="submit">Change password</button>
        {message && <p className="form-message">{message}</p>}
      </form>
    </div>
  );
};

export default ChangePasswordPage;
