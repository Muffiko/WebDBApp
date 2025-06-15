import React, { useEffect, useState } from "react";
import Sidebar from "../components/Sidebar";
import "./styles/WorkersPage.css";
import WorkersList from "../components/WorkersList";
import { useWorkersApi } from "../api/worker";

const WorkersPage = () => {
  const { getWorkers } = useWorkersApi();
  const [workers, setWorkers] = useState([]);

  const loadWorkers = async () => {
    try {
      const data = await getWorkers();
      const onlyWorkers = data.filter(w => w.role === "Worker");
      const mapped = onlyWorkers.map((w, wdx) => ({
        id: wdx + 1,
        workerId: w.id,
        name: `${w.firstName} ${w.lastName}`,
        email: w.email,
        status: "Available" // TODO: Replace with actual status logic
      }));
      setWorkers(mapped);
    } catch (error) {
      console.error("Error loading workers:", error);
    }
  };

  useEffect(() => {
    loadWorkers();
  }, []);

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