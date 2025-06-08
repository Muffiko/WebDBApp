import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import "./styles/TasksPage.css";
import TaskCard from "../components/TaskCard";


const menuItems = [
    { path: "/tasks", label: "My Tasks", icon: "🟦" },
    { path: "/profile", label: "Profile", icon: "👤" }
];

const mockTasks = [
    {
        id: 1,
        name: "Komputer",
        status: "Open",
        manager: "Jan Kowalski",
        dateCreated: "01/01/2025"
    },
    {
        id: 2,
        name: "Komputer",
        status: "In progress",
        manager: "Marcin Kowalski",
        dateCreated: "01/01/2025"
    },
    {
        id: 3,
        name: "Komputer",
        status: "In progress",
        manager: "Kamil Kowalski",
        dateCreated: "01/01/2025"
    }
];

const TasksPage = () => {
    const [filters, setFilters] = useState({
        name: "",
        status: "",
        manager: ""
    });

    const [showModal, setShowModal] = useState(false);

    const filtered = mockTasks.filter((r) =>
        r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
        (filters.status === "" || r.status === filters.status) &&
        (filters.manager === "" || r.manager === filters.manager)
    );

    return (
        <div className="tasks-container">
            <Sidebar menuItems={menuItems} />
            <div className="tasks-page">
                <h1 className="tasks-title">Tasks</h1>

                <FilterBar
                    filters={filters}
                    setFilters={setFilters}
                    createLabel={null}
                    selects={[
                        {
                            key: "status",
                            label: "Status:",
                            options: ["Open", "In progress", "Closed"]
                        },
                    ]}
                />


                <div className="tasks-list">
                    {filtered.map((req) => (
                        <TaskCard key={req.id} {...req} />
                    ))}
                </div>
                
            </div>
        </div>
    );
};

export default TasksPage;
