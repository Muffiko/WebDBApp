import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import "./styles/WorkersPage.css";
import WorkersList from "../components/WorkersList";

const menuItems = [
  { path: "/new-requests", label: "New Requests", icon: "🟦" },
  { path: "/open-requests", label: "Open Requests", icon: "📂" },
  { path: "/workers", label: "Workers", icon: "🗂️" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const initialWorkers = [
  { id: 1, name: "Mariusz Kowalski", status: "Available" },
  { id: 2, name: "Jan Nowak", status: "Unavailable" },
  { id: 3, name: "Anna Wiśniewska", status: "Available" },
];


const WorkersPage = () => {
  const [workers] = useState(initialWorkers);
  return (
    <div className="workers-container">
      <Sidebar />
      <div className="workers-page">
        <h1 className="workers-title">Workers</h1>
        <WorkersList workers={workers} />
        </div>
      </div>
  );
};

export default WorkersPage;
