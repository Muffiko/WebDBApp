import React, { useState, useEffect } from "react";
import "./styles/EditForms.css";
import { useUserApi } from "../api/user";
import { useNavigate } from "react-router-dom";

const EditPersonalInfoPage = () => {
  const { getUserProfile, updateUserInfo } = useUserApi();
  const navigate = useNavigate();

  const [form, setForm] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
  });

  const [initialData, setInitialData] = useState(null);
  const [errors, setErrors] = useState({});
  const [message, setMessage] = useState("");
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadProfile = async () => {
      try {
        const data = await getUserProfile();
        const initial = {
          firstName: data.firstName || "",
          lastName: data.lastName || "",
          email: data.email || "",
          phoneNumber: data.phoneNumber || "",
        };
        setForm(initial);
        setInitialData(initial);
      } catch (err) {
        setMessage("Could not load user data.");
      } finally {
        setLoading(false);
      }
    };

    loadProfile();
  }, []);

  const handleChange = (e) => {
    setForm({ ...form, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setMessage("");
    setErrors({});
  
    const hasChanges = Object.keys(form).some(key => form[key] !== initialData[key]);
    if (!hasChanges) {
      navigate("/profile");
      return;
    }
  
    const payload = {};
    for (const key in form) {
      payload[key] = form[key] !== initialData[key] ? form[key] : null;
    }
  
    try {
      const msg = await updateUserInfo(payload);
      navigate("/profile", { state: { message: msg.message } });
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

        {loading ? (
          <p className="form-message">Loading...</p>
        ) : (
          <>
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
          </>
        )}
      </form>
    </div>
  );
};

export default EditPersonalInfoPage;