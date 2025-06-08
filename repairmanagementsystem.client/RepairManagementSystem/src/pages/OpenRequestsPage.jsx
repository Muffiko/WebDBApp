import React from "react";
import Sidebar from "../components/Sidebar";
import "./styles/OpenRequestsPage.css";

const OpenRequestsPage = () => {
  return (
    <div className="open-requests-container">
      <Sidebar />
      <div className="open-requests-page">
        <h1 className="open-requests-title">Open Requests</h1>

        <div className="open-requests-card">
          <p className="open-requests-description">
            This is where open requests will be displayed.
          </p>
        </div>
      </div>
    </div>
  );
};

export default OpenRequestsPage;
