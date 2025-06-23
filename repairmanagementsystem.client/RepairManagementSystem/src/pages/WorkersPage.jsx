import React, { useEffect, useState } from "react";
import Sidebar from "../components/Sidebar";
import "./styles/WorkersPage.css";
import WorkersList from "../components/WorkersList";
import { useWorkersApi } from "../api/worker";
import { useRepairRequestApi } from "../api/repairRequests";

const WorkersPage = () => {
  const { getWorkers } = useWorkersApi();
  const [workers, setWorkers] = useState([]);

  const { getActiveRepairRequests } = useRepairRequestApi();
  const [activities, setActivities] = useState([]);
  const loadWorkers = async () => {
    try {
      const data = await getWorkers();
      const mapped = data.map(w => ({
        workerId: w.workerId,
        firstName: w.user.firstName,
        lastName: w.user.lastName,
        email: w.user.email,
        isAvailable: w.isAvailable,
        expertise: w.expertise,
      }));
      setWorkers(mapped);
    } catch (error) {
      console.error("Error loading workers:", error);
    }
  };

  const loadActivities = async () => {
    try {
      const reqs = await getActiveRepairRequests();
      const allActs = reqs.flatMap(r =>
        (r.repairActivities || []).map(a => ({
          repairRequestId: r.repairRequestId,
          repairActivityId: a.repairActivityId,
          sequenceNumber: a.sequenceNumber,
          name: a.name,
          status: a.status.toUpperCase(),
          workerId: a.workerId,
        }))
      );
      setActivities(allActs);
    } catch (error) {
      console.error("Error loading activities:", error);
    }
  };

  useEffect(() => {
    loadWorkers();
    loadActivities();
  }, []);

  return (
    <div className="workers-container">
      <Sidebar />
      <main className="workers-page">
        <h1 className="workers-title">Workers</h1>
        <WorkersList workers={workers} activities={activities} />
      </main>
    </div>
  );
};

export default WorkersPage;