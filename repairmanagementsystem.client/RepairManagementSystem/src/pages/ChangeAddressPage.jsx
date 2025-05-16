import React from "react";
import "./styles/EditForms.css";

const ChangeAddressPage = () => {
  return (
    <div className="form-page">
      <form className="form-card">
        <h2>Change address</h2>
        <input type="text" placeholder="Country" />
        <input type="text" placeholder="City" />
        <input type="text" placeholder="Postal Code" />
        <input type="text" placeholder="Street" />
        <div className="row">
          <input type="text" placeholder="House Number" />
          <input type="text" placeholder="Apartment" />
        </div>
        <button type="submit">Confirm change</button>
      </form>
    </div>
  );
};

export default ChangeAddressPage;
