import React, { useState } from "react";
import "./styles/EditForms.css";
import { useUserApi } from "../api/user";
import { useNavigate } from "react-router-dom";

const ChangeAddressPage = () => {
  const { updateUserAddress } = useUserApi();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    country: "",
    city: "",
    postalCode: "",
    street: "",
    houseNumber: "",
    apartNumber: "",
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
      await updateUserAddress(form);
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
        <h2>Change address</h2>

        <div className="input-wrapper">
          <label>Country</label>
          <input
            type="text"
            name="country"
            value={form.country}
            onChange={handleChange}
          />
          {errors.country && <small className="error">{errors.country}</small>}
        </div>

        <div className="input-wrapper">
          <label>City</label>
          <input
            type="text"
            name="city"
            value={form.city}
            onChange={handleChange}
          />
          {errors.city && <small className="error">{errors.city}</small>}
        </div>

        <div className="input-wrapper">
          <label>Postal Code</label>
          <input
            type="text"
            name="postalCode"
            value={form.postalCode}
            onChange={handleChange}
          />
          {errors.postalCode && <small className="error">{errors.postalCode}</small>}
        </div>

        <div className="input-wrapper">
          <label>Street</label>
          <input
            type="text"
            name="street"
            value={form.street}
            onChange={handleChange}
          />
          {errors.street && <small className="error">{errors.street}</small>}
        </div>

        <div className="row">
          <div className="input-wrapper">
            <label>House Number</label>
            <input
              type="text"
              name="houseNumber"
              value={form.houseNumber}
              onChange={handleChange}
            />
            {errors.houseNumber && <small className="error">{errors.houseNumber}</small>}
          </div>

          <div className="input-wrapper">
            <label>Apartment</label>
            <input
              type="text"
              name="apartNumber"
              value={form.apartNumber}
              onChange={handleChange}
            />
            {errors.apartNumber && <small className="error">{errors.apartNumber}</small>}
          </div>
        </div>

        <button type="submit">Confirm change</button>
        {message && <p className="form-message">{message}</p>}
      </form>
    </div>
  );
};

export default ChangeAddressPage;
