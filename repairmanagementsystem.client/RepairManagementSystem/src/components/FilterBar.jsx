import React from "react";

const FilterBar = ({ filters, setFilters, onCreate }) => {
  return (
    <div className="requests-top-bar">
      <div className="filter-left">
        <input
          type="text"
          placeholder="Search..."
          value={filters.name}
          onChange={(e) => setFilters({ ...filters, name: e.target.value })}
        />
        <select
          value={filters.status}
          onChange={(e) => setFilters({ ...filters, status: e.target.value })}
        >
          <option value="">Status:</option>
          <option value="Open">Open</option>
          <option value="In progress">In progress</option>
          <option value="Closed">Closed</option>
        </select>
        <select
          value={filters.manager}
          onChange={(e) => setFilters({ ...filters, manager: e.target.value })}
        >
          <option value="">Manager:</option>
          <option value="Jan Kowalski">Jan Kowalski</option>
          <option value="Marcin Kowalski">Marcin Kowalski</option>
          <option value="Kamil Kowalski">Kamil Kowalski</option>
        </select>
      </div>
      <button className="create-button" onClick={onCreate}>+ Create</button>
    </div>
  );
};

export default FilterBar;
