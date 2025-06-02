import React from "react";
import Sidebar from "../components/Sidebar";
import "./styles/TasksPage.css";

const menuItems = [
  { path: "/tasks", label: "My Tasks", icon: "🟦" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const TasksPage = () => {
  return (
    <div className="tasks-container">
      <Sidebar menuItems={menuItems} />
      <div className="tasks-page">
        <h1 className="tasks-title">Tasks</h1>

        <div className="tasks-card">
          <p className="tasks-description">
            This is where tasks will be displayed.
          </p>
        </div>
      </div>
    </div>
  );
};

export default TasksPage;
