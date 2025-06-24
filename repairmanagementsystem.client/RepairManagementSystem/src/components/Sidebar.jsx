import React from "react";
import { NavLink, useLocation, useNavigate } from "react-router-dom";
import "./styles/Sidebar.css";
import { useAuth } from "../contexts/AuthContext";

const Sidebar = () => {
  const location = useLocation();
  const navigate = useNavigate();
  const { user, logout } = useAuth();

  const handleLogout = () => {
    logout();
    navigate("/");
  };

  const getMenuItems = () => {
    switch (user?.role) {
      case "Manager":
        return [
          { path: "/new-requests", label: "New Requests", icon: "🟦" },
          { path: "/open-requests", label: "Open Requests", icon: "📂" },
          { path: "/finished-requests", label: "Finished Requests", icon: "📁" },
          { path: "/workers", label: "Workers", icon: "🗂️" },
          { path: "/profile", label: "Profile", icon: "👤" }
        ];
      case "Worker":
        return [
          { path: "/tasks", label: "My Tasks", icon: "🟦" },
          { path: "/profile", label: "Profile", icon: "👤" }
        ];
      case "Admin":
        return [
          { path: "/admin", label: "Admin panel", icon: "🔧 " },
          { path: "/profile", label: "Profile", icon: "👤" }
        ];
      default:
        // default to "User"
        return [
          { path: "/objects", label: "My repair objects", icon: "🧰" },
          { path: "/requests", label: "My requests", icon: "📋" },
          { path: "/profile", label: "Profile", icon: "👤" }
        ];
    }
  };

  const menuItems = getMenuItems();

  return (
    <div className="sidebar">
      <div className="sidebar-logo">RMS</div>

      <nav className="sidebar-nav">
        {menuItems.map((item) => (
          <NavLink
            key={item.path}
            to={item.path}
            className={`sidebar-item ${location.pathname === item.path ? "active" : ""}`}
          >
            <span className="sidebar-icon">{item.icon}</span>
            <span className="sidebar-text">{item.label}</span>
          </NavLink>
        ))}
      </nav>

      <div className="sidebar-footer">
        {user && (
          <div className="user-info">
            <div className="logged-as">Logged as:</div>
            <div className="user-email">
              {user.firstName} ({user.email})
            </div>
          </div>
        )}
        <button className="logout-button" onClick={handleLogout}>
          Logout
        </button>
      </div>
    </div>
  );
};

export default Sidebar;