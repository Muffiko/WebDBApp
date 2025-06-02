import React from "react";
import Sidebar from "../components/Sidebar";
import "./styles/OpenRequestsPage.css";

const menuItems = [
  { path: "/new-requests", label: "New Requests", icon: "🟦" },
  { path: "/open-requests", label: "Open Requests", icon: "📂" },
  { path: "/workers", label: "Workers", icon: "🗂️" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const OpenRequestsPage = () => {
  return (
    <div className="open-requests-container">
      <Sidebar menuItems={menuItems} />
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
