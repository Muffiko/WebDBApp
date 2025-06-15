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
        {activities.map((act, index) => (
          <ActivityCard
            key={act.repairActivityId}
            id={index + 1}
            //name={act.name}
            workerId={act.workerId}
            status={act.status}
            description={act.description}
            activityType={act.repairActivityType.repairActivityTypeId}
            createdAt={act.createdAt}
            result={act.result}
          //startedAt={act.startedAt}
          //finishedAt={act.finishedAt}
          />
        ))}
      </div>
    </div>
  );
};

export default ActivitiesList;
