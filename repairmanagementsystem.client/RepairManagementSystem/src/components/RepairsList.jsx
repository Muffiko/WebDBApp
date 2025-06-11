import React from "react";
import RepairCard from "./RepairCard";
import "./styles/RepairsList.css";

const RepairsList = ({ repairs }) => (
  <div className="repairs-wrapper">
    <div className="repairs-cards">
      {repairs.map((r, index) => (
        <RepairCard
          key={r.id}
          id={index + 1}
          name={r.name}
          status={r.status}
          manager={r.manager}
          dateCreated={r.dateCreated}
        />
      ))}
    </div>
  </div>
);

export default RepairsList;
