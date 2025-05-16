import React from "react";
import "./styles/EditForms.css";

const EditPersonalInfoPage = () => {
  return (
    <div className="form-page">
      <form className="form-card">
        <h2>Edit personal info</h2>
        <div className="row">
          <input type="text" placeholder="Name" />
          <input type="text" placeholder="Surname" />
        </div>
        <input type="email" placeholder="Email" />
        <input type="text" placeholder="Phone number" />
        <button type="submit">Update</button>
      </form>
    </div>
  );
};

export default EditPersonalInfoPage;
