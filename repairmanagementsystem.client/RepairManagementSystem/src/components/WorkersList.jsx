import React from "react";
import WorkerCard from "./WorkerCard";
import "./styles/WorkersList.css";

const WorkersList = ({ workers }) => (
    <div className="workers-wrapper">
        <div className="workers-cards">
            {workers.map((w, index) => (
                <WorkerCard key={w.id} id={index + 1} name={w.name} status={w.status} />
            ))}
        </div>
    </div>
);

export default WorkersList;
