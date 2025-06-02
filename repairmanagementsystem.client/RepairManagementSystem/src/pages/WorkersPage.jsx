import React from "react";
import Sidebar from "../components/Sidebar";
import "./styles/WorkersPage.css";

const menuItems = [
  { path: "/new-requests", label: "New Requests", icon: "🟦" },
  { path: "/open-requests", label: "Open Requests", icon: "📂" },
  { path: "/workers", label: "Workers", icon: "🗂️" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const WorkersPage = () => {
  return (
    <div className="workers-container">
      <Sidebar menuItems={menuItems} />
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
