import React from "react";
import { NavLink, useLocation } from "react-router-dom";
import "./styles/Sidebar.css";

const Sidebar = ({ menuItems }) => {
  const location = useLocation();

  return (
    <div className="sidebar">
      <div className="sidebar-logo">LOGO</div>
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
    </div>
  );
};

export default Sidebar;
