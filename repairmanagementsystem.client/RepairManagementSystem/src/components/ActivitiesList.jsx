import React from "react";
import ActivityCard from "./ActivityCard";
import "./styles/ActivitiesList.css";

const ActivitiesList = ({ activities, onAdd, onUpdate }) => {
  const sortedActivities = [...activities].sort(
    (a, b) => Number(b.sequenceNumber) - Number(a.sequenceNumber)
  );

  return (
    <div className="activities-wrapper">
      <div className="activities-header">
        <h3>Activities:</h3>
        <button className="activities-add" onClick={onAdd}>
          Add
        </button>
      </div>

      <div className="activities-cards">
        {sortedActivities.map(act => (
          <ActivityCard
            key={act.repairActivityId}
            id={act.repairActivityId}
            sequenceNumber={act.sequenceNumber}
            name={act.name}
            activityType={act.activityType}
            description={act.description}
            workerId={act.workerId}
            status={act.status}
            createdAt={act.createdAt}
            startedAt={act.startedAt}
            finishedAt={act.finishedAt}
            onUpdate={onUpdate}
          />
        ))}
      </div>
    </div>
  );
};

export default ActivitiesList;
