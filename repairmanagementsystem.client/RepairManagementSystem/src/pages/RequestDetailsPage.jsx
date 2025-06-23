import React, { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import { useRepairRequestApi } from "../api/repairRequests";
import "./styles/RequestDetailsPage.css";

const statusColorMap = {
  OPEN: "blue",
  "IN PROGRESS": "yellow",
  CLOSED: "gray",
  CANCELLED: "red",
  COMPLETED: "green",
  "TO DO": "#4ade00"
};

const formatDate = (isoString) => {
  if (!isoString) return "-";
  return new Date(isoString).toLocaleDateString("en-GB");
};

const RequestDetailsPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { getRepairRequestById } = useRepairRequestApi();

  const [data, setData] = useState(null);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    const loadData = async () => {
      try {
        const res = await getRepairRequestById(id);
        setData(res);
      } catch (err) {
        console.error("Failed to load repair request:", err);
      } finally {
        setLoading(false);
      }
    };
    loadData();
  }, [id]);

  if (loading || !data) {
    return (
      <div className="repair-container">
        <Sidebar />
        <div className="repair-page">
          <h1 className="repair-title">Repair</h1>
          <div className="repair-card">
            <p>Loading repair details...</p>
          </div>
        </div>
      </div>
    );
  }

  const statusColor = statusColorMap[data.status] || "gray";
  const obj = data.repairObject;

  return (
    <div className="repair-container">
      <Sidebar />
      <div className="repair-page">
        <h1 className="repair-title">Repair</h1>

        <div className="repair-card">
          <div className={`repair-status-label status--${statusColor}`}>
            {data.status}
          </div>

          <div className="repair-section">
            <h3>Details:</h3>
            <div className="repair-grid">
              <div>
                <p><strong>Name:</strong> {obj?.name || "Unknown"}</p>
                <p><strong>Object type:</strong> {obj?.repairObjectType?.name || "Unknown"}</p>
                <p><strong>Customer:</strong> ID {obj?.customerId ?? "Unknown"}</p>
              </div>
              <div>
                <p><strong>Manager:</strong> {data.managerName || "Not assigned"}</p>
                <p><strong>Created:</strong> {formatDate(data.createdAt)}</p>
                <p><strong>Started:</strong> {formatDate(data.startedAt)}</p>
                <p><strong>Finished:</strong> {formatDate(data.finishedAt)}</p>
              </div>
            </div>
          </div>

          <div className="repair-section">
            <h3>Description:</h3>
            <p className="repair-description">{data.description || "No description provided."}</p>
          </div>

          <div className="repair-section">
            <h3>Result:</h3>
            <p className="repair-description">{data.result || "Not completed yet."}</p>
          </div>

          <button className="repair-back" onClick={() => navigate(-1)}>
            ← Go back
          </button>
        </div>
      </div>
    </div>
  );
};

export default RequestDetailsPage;