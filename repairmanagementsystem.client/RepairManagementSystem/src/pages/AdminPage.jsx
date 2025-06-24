import React, { useEffect, useState } from "react";
import Sidebar from "../components/Sidebar";
import { useUserApi } from "../api/user";
import { useRepairObjectTypesApi } from "../api/repairObjectTypes";
import "./styles/AdminPage.css";

const AdminPage = () => {
  const { getUsers, updateUserRole } = useUserApi();
  const {
    getRepairObjectTypes,
    addRepairObjectType,
    deleteRepairObjectType
  } = useRepairObjectTypesApi();

  const [types, setTypes] = useState([]);
  const [users, setUsers] = useState([]);
  const [newTypeMnemonic, setNewTypeMnemonic] = useState("");
  const [newTypeName, setNewTypeName] = useState("");
  const [error, setError] = useState("");
  const [userSearch, setUserSearch] = useState("");

  const loadTypes = async () => {
    try {
      const data = await getRepairObjectTypes();
      setTypes(data);
    } catch {
      setError("Failed to load types.");
    }
  };

  const loadUsers = async () => {
    try {
      const data = await getUsers();
      setUsers(data);
    } catch {
      setError("Failed to load users.");
    }
  };

  useEffect(() => {
    loadTypes();
    loadUsers();
  }, []);

  const handleAddType = async () => {
    if (!newTypeMnemonic.trim() || !newTypeName.trim()) return;
    try {
      await addRepairObjectType(newTypeMnemonic.trim(), newTypeName.trim());
      setNewTypeMnemonic("");
      setNewTypeName("");
      loadTypes();
    } catch {
      setError("Failed to add type.");
    }
  };

  const handleDeleteType = async (id) => {
    try {
      await deleteRepairObjectType(id);
      loadTypes();
    } catch {
      setError("Failed to delete type.");
    }
  };

  const handleRoleChange = async (userId, role) => {
    try {
      await updateUserRole(userId, role);
      loadUsers();
    } catch {
      setError("Failed to update user role.");
    }
  };

  const filteredUsers = users.filter((user) =>
    `${user.firstName} ${user.lastName} ${user.email}`
      .toLowerCase()
      .includes(userSearch.toLowerCase())
  );

  return (
    <div className="admin-container">
      <Sidebar />
      <div className="admin-page">
        <h1 className="admin-title">Admin Panel</h1>

        <div className="admin-panels">
          {/* === Panel typów === */}
          <div className="admin-panel">
            <h2>Repair Object Types</h2>
            <div className="add-type-form">
              <input
                type="text"
                placeholder="Mnemonic (e.g. XYZ)"
                value={newTypeMnemonic}
                onChange={(e) => setNewTypeMnemonic(e.target.value.toUpperCase())}
              />
              <input
                type="text"
                placeholder="Display name"
                value={newTypeName}
                onChange={(e) => setNewTypeName(e.target.value)}
              />
              <button onClick={handleAddType}>Add</button>
            </div>
            <ul className="types-list">
              {types.map((t) => (
                <li key={t.repairObjectTypeId}>
                  {t.repairObjectTypeId.toUpperCase()} – {t.name}
                  <button className="delete-btn" onClick={() => handleDeleteType(t.repairObjectTypeId)}>DELETE</button>
                </li>
              ))}
            </ul>
          </div>

          {/* === Panel użytkowników === */}
          <div className="admin-panel">
            <h2>Users</h2>

            <input
              type="text"
              placeholder="Search users..."
              value={userSearch}
              onChange={(e) => setUserSearch(e.target.value)}
              className="admin-user-search"
            />

            <div className="users-list">
              {filteredUsers.map((user) => (
                <div key={user.userId} className="user-item">
                  <span>
                    {user.firstName} {user.lastName} – {user.email}
                  </span>
                  <select
                    value={user.role}
                    onChange={(e) => handleRoleChange(user.userId, e.target.value)}
                  >
                    <option value="Customer">Customer</option>
                    <option value="Manager">Manager</option>
                    <option value="Worker">Worker</option>
                    <option value="Admin">Admin</option>
                  </select>
                </div>
              ))}
            </div>
          </div>

          {/* === Panel statystyk === */}
          <div className="admin-panel">
            <h2>Statistics</h2>
            <div className="stats-list">
              <div className="stat-item">
                👥 <strong>Total users:</strong> {users.length}
              </div>
              <div className="stat-item">
                🛠️ <strong>Types defined:</strong> {types.length}
              </div>
              <div className="stat-item">
                🧑‍💼 <strong>Admins:</strong>{" "}
                {users.filter((u) => u.role === "Admin").length}
              </div>
              <div className="stat-item">
                📂 <strong>Managers:</strong>{" "}
                {users.filter((u) => u.role === "Manager").length}
              </div>
              <div className="stat-item">
                🧰 <strong>Workers:</strong>{" "}
                {users.filter((u) => u.role === "Worker").length}
              </div>
              <div className="stat-item">
                👤 <strong>Customers:</strong>{" "}
                {users.filter((u) => u.role === "Customer").length}
              </div>
            </div>
          </div>
        </div>

        {error && <div className="admin-error">{error}</div>}
      </div>
    </div>
  );
};

export default AdminPage;
