import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import { useRepairActivityApi } from "../api/repairActivity";
import { useRepairRequestApi } from "../api/repairRequests";
import { useManagerApi } from "../api/manager";
import RequestInfoCard from "../components/RequestInfoCard";
import TaskCardDetail from "../components/TaskCardDetail";
import "./styles/MyTaskDetailPage.css";

const MyTaskDetailPage = () => {
  const { number } = useParams();
  const { getMyRepairActivities } = useRepairActivityApi();
  const { getRepairRequestById } = useRepairRequestApi();
  const { getManagers } = useManagerApi();

  const [activity, setActivity] = useState(null);
  const [request, setRequest] = useState(null);
  const [relatedTasks, setRelatedTasks] = useState([]);
  const [managerName, setManagerName] = useState("Unknown");
  const [error, setError] = useState(null);
  const [loading, setLoading] = useState(true);
  const [expandedTaskId, setExpandedTaskId] = useState(null);

  const fetchDetails = async () => {
    setLoading(true);
    setError(null);
    try {
      const activities = await getMyRepairActivities();
      const index = parseInt(number, 10) - 1;
      if (isNaN(index) || index < 0 || index >= activities.length) {
        throw new Error("Invalid task number.");
      }

      const selectedActivity = activities[index];
      setActivity(selectedActivity);

      const requestDetails = await getRepairRequestById(selectedActivity.repairRequestId);
      setRequest(requestDetails);

      if (requestDetails.managerId) {
        const managers = await getManagers();
        const matched = managers.find(m => m.managerId === requestDetails.managerId);
        if (matched?.user) {
          const { firstName, lastName } = matched.user;
          setManagerName(`${firstName} ${lastName}`);
        }
      }

      if (requestDetails.repairActivities?.length) {
        const myTasks = requestDetails.repairActivities.filter(
          task => task.workerId === selectedActivity.workerId
        );
        setRelatedTasks(myTasks);
      }
    } catch (err) {
      setError(err.message || "Error loading task details.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    fetchDetails();
  }, [number]);

  return (
    <div className="task-detail-container">
      <Sidebar />
      <div className="task-detail-page">
        {loading && <p>Loading...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}

        {!loading && !error && activity && request ? (
          <>
            <h1>Repair Request</h1>
            <RequestInfoCard request={request} managerName={managerName} />

            <h3>Your tasks related to this request:</h3>
            {relatedTasks.map(task => (
              <TaskCardDetail
                key={task.repairActivityId}
                task={task}
                expanded={expandedTaskId === task.repairActivityId}
                onToggle={() =>
                  setExpandedTaskId(prev =>
                    prev === task.repairActivityId ? null : task.repairActivityId
                  )
                }
                onStatusChange={fetchDetails}
              />
            ))}
          </>
        ) : null}
      </div>
    </div>
  );
};

export default MyTaskDetailPage;
