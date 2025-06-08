import React from "react";
import Sidebar from "../components/Sidebar";
import "./styles/WorkersPage.css";

const WorkersPage = () => {
  return (
    <div className="workers-container">
      <Sidebar />
      <div className="workers-page">
        <h1 className="workers-title">Workers</h1>

        <div className="workers-card">
          <p className="workers-description">
            This is where workers will be displayed.
          </p>
        </div>
      </div>
    </div>
  );
};

export default WorkersPage;
