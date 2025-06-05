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
        {activities.map((act) => (
          <ActivityCard
            key={act.id}
            id={act.id}
            name={act.name}
            worker={act.worker}
            status={act.status}
          />
        ))}
      </div>
    </div>
  );
};

export default ActivitiesList;
