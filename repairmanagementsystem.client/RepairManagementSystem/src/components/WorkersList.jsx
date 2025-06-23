import React from "react";
import WorkerCard from "./WorkerCard";
import "./styles/WorkersList.css";

const WorkersList = ({ workers, activities }) => (
    <div className="workers-wrapper">
        <div className="workers-cards">
            {workers.map((w, index) => (
                <WorkerCard key={w.workerId} id={index + 1} name={w.firstName + " " + w.lastName} email={w.email} status={w.isAvailable ? "Available" : "Unavailable"} expertise={w.expertise} activities={activities.filter(a => a.workerId === w.workerId)} />
            ))}
        </div>
    </div>
);

export default WorkersList;
