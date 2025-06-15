import React from "react";
import ActivityCard from "./ActivityCard";
import "./styles/ActivitiesList.css";

const ActivitiesList = ({ activities, onAdd }) => {
  return (
    <div className="activities-wrapper">
      <div className="activities-header">
        <h3>Activities:</h3>
        <button className="activities-add" onClick={onAdd}>
          Add
        </button>
      </div>

      <div className="activities-cards">
        {activities.map(act => (
          <ActivityCard
            key={act.repairActivityId}
            id={act.repairActivityId}
            name={act.name}
            activityType={act.activityType}
            description={act.description}
            worker={act.workerId}
            status={act.status}
            createdAt={act.createdAt}
            startedAt={act.startedAt}
            finishedAt={act.finishedAt}
          />
        ))}
      </div>
    </div>
  );
};

export default ActivitiesList;
