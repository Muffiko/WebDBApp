import React from "react";
import "./styles/FilterBar.css";

const FilterBar = ({ filters, setFilters, selects = [], onCreate, createLabel = "+ Create" }) => {
  return (
    <div className="requests-top-bar">
      <div className="filter-left">
        <input
          type="text"
          placeholder="Search..."
          value={filters.name || ""}
          onChange={(e) => setFilters({ ...filters, name: e.target.value })}
        />

        {selects.map((select) => (
          <select
            key={select.key}
            value={filters[select.key] || ""}
            onChange={(e) => setFilters({ ...filters, [select.key]: e.target.value })}
          >
            <option value="">{select.label}</option>
            {select.options.map((option) => (
              <option key={option} value={option}>
                {option}
              </option>
            ))}
          </select>
        ))}
      </div>

      <button className="create-button" onClick={onCreate}>
        {createLabel}
      </button>
    </div>
  );
};

export default FilterBar;
