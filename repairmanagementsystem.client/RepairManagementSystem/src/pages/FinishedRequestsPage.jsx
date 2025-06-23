import React, { useState, useEffect } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import RepairsList from "../components/RepairsList";
import "./styles/FinishedRequestsPage.css";
import { useManagerApi } from "../api/manager";
import { useRepairRequestApi } from "../api/repairRequests";

const FinishedRequestsPage = () => {
    const [filters, setFilters] = useState({
        name: "",
        status: "",
        manager: "",
    });

    const { getManagers } = useManagerApi();
    const [managers, setManagers] = useState([]);

    const loadManagers = async () => {
        try {
            const data = await getManagers();
            const mapped = data.map(m => ({
                managerId: m.managerId,
                firstName: m.user.firstName,
                lastName: m.user.lastName,
            }));
            setManagers(mapped);
        } catch (error) {
            console.error("Error loading managers:", error);
        }
    };

    const { getFinishedRepairRequests } = useRepairRequestApi();
    const [requests, setRequests] = useState([]);

    const loadFinishedRequests = async () => {
        try {
            const data = await getFinishedRepairRequests();
            const mgrMap = managers.reduce((acc, m) => {
                acc[m.managerId] = `${m.firstName} ${m.lastName}`;
                return acc;
            }, {});
            const mapped = data.map(r => ({
                repairRequestId: r.repairRequestId,
                name: r.repairObjectName,
                createdAt: new Date(r.createdAt).toLocaleDateString(),
                status: r.status,
                manager: mgrMap[r.managerId]
            }));
            setRequests(mapped);
        } catch (err) {
            console.error("Failed to load finished requests:", err);
        }
    };

    useEffect(() => {
        loadManagers();
    }, []);

    useEffect(() => {
        if (managers.length > 0) {
            loadFinishedRequests();
        }
    }, [managers]);

    const managersOptions = managers.map(
        m => `${m.firstName} ${m.lastName}`
    );

    const filtered = requests.filter(
        r =>
            r.name.toLowerCase().includes(filters.name.toLowerCase()) &&
            (filters.status === "" || r.status === filters.status) &&
            (filters.manager === "" || r.manager === filters.manager)
    );

    return (
        <div className="finished-requests-container">
            <Sidebar />
            <main className="finished-requests-page">
                <h1 className="finished-requests-title">Finished Requests</h1>
                <FilterBar
                    filters={filters}
                    setFilters={setFilters}
                    createLabel={null}
                    selects={[
                        {
                            key: "status",
                            label: "Status:",
                            options: ["CANCELLED", "COMPLETED"]
                        },
                        {
                            key: "manager",
                            label: "Manager:",
                            options: managersOptions,
                        },
                    ]}
                />
                <RepairsList repairs={filtered} />
            </main>
        </div>
    );
};

export default FinishedRequestsPage;
