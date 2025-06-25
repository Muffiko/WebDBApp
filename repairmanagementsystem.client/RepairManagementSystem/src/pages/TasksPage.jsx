import React, { useEffect, useState } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import "./styles/TasksPage.css";
import TaskCard from "../components/TaskCard";
import { useRepairActivityApi } from "../api/repairActivity";

const TasksPage = () => {
    const { getMyRepairActivities } = useRepairActivityApi();
    const [tasks, setTasks] = useState([]);
    const [filters, setFilters] = useState({
        name: "",
        status: "",
        manager: ""
    });
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchMyTasks = async () => {
            try {
                const data = await getMyRepairActivities();
                setTasks(data);
            } catch (err) {
                setError(err.message || "Error while loading tasks");
            } finally {
                setLoading(false);
            }
        };

        fetchMyTasks();
    }, []);

    const filtered = tasks.filter((task) =>
        task.name.toLowerCase().includes(filters.name.toLowerCase()) &&
        (filters.status === "" || task.status === filters.status) &&
        (filters.manager === "" || task.manager === filters.manager)
    );

    return (
        <div className="tasks-container">
            <Sidebar />
            <div className="tasks-page">
                <h1 className="tasks-title">My Tasks</h1>

                <FilterBar
                    filters={filters}
                    setFilters={setFilters}
                    createLabel={null}
                    selects={[
                        {
                            key: "status",
                            label: "Status:",
                            options: ["TO DO", "IN PROGRESS", "CANCELLED", "COMPLETED"]
                        },
                    ]}
                />

                {loading && <p>Loading...</p>}
                {error && <p style={{ color: "red" }}>{error}</p>}

                <div className="tasks-list">
                    {!loading && !error && filtered.map((task, index) => (
                        <TaskCard
                            key={task.repairActivityId}
                            {...task}
                            index={index + 1}
                        />
                    ))}
                </div>
            </div>
        </div>
    );
};

export default TasksPage;
