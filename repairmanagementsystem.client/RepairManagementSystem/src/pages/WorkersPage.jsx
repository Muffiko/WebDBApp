import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import "./styles/WorkersPage.css";
import WorkersList from "../components/WorkersList";

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
      <main className="workers-page">
        <h1 className="workers-title">Workers</h1>
        <WorkersList workers={workers} />
      </main>
    </div>
  );
};

export default WorkersPage;