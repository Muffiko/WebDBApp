import React from "react";
import RepairCard from "./RepairCard";
import "./styles/RepairsList.css";

const RepairsList = ({ repairs }) => (
  <div className="repairs-wrapper">
    <div className="repairs-cards">
      {repairs.map((r, index) => (
        <RepairCard
          key={index + 1}
          id={index + 1}
          requestId={r.repairRequestId}
          name={r.name}
          status={r.status}
          manager={r.manager}
          createdAt={r.createdAt}
        />
      ))}
    </div>
  </div>
);

export default RepairsList;
