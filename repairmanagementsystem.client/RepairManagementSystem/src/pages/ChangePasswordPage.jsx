import React from "react";
import "./styles/EditForms.css";

const ChangePasswordPage = () => {
  return (
    <div className="form-page">
      <form className="form-card">
        <h2>Change password</h2>
        <input type="password" placeholder="Old password" />
        <input type="password" placeholder="New password" />
        <input type="password" placeholder="Confirm new password" />
        <button type="submit">Change password</button>
      </form>
    </div>
  );
};

export default ChangePasswordPage;
