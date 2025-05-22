import React, { useState } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import AddRepairObjectModal from "../components/AddRepairObjectModal";
import NewRepairModal from "../components/NewRepairModal";
import "./styles/MyRepairObjectsPage.css";

const menuItems = [
  { path: "/objects", label: "My repair objects", icon: "🧰" },
  { path: "/requests", label: "My requests", icon: "📋" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const MyRepairObjectsPage = () => {
  const [objects, setObjects] = useState([]);
  const [filters, setFilters] = useState({ name: "", type: "" });
  const [showAddModal, setShowAddModal] = useState(false);
  const [selectedObject, setSelectedObject] = useState(null);

  const handleAddObject = ({ name, type }) => {
    setObjects([...objects, { id: Date.now(), name, type }]);
  };

  const filteredObjects = objects.filter((obj) =>
    obj.name.toLowerCase().includes(filters.name.toLowerCase()) &&
    (filters.type === "" || obj.type === filters.type)
  );

  return (
    <div className="objects-container">
      <Sidebar menuItems={menuItems} />
      <div className="objects-page">
        <h1 className="objects-title">My repair objects</h1>

        <FilterBar
          filters={filters}
          setFilters={setFilters}
          onCreate={() => setShowAddModal(true)}
          createLabel="+ Add object"
          selects={[
            {
              key: "type",
              label: "Type:",
              options: ["Laptop", "Phone", "Other"]
            }
          ]}
        />

        <div className="object-list">
          {filteredObjects.map((obj) => (
            <div key={obj.id} className="object-card">
              <div className="object-info">
                <h3>{obj.name}</h3>
                <p>Type: {obj.type}</p>
              </div>
              <button onClick={() => setSelectedObject(obj)}>
                Create request
              </button>
            </div>
          ))}
        </div>

        {showAddModal && (
          <AddRepairObjectModal
            onClose={() => setShowAddModal(false)}
            onSubmit={(data) => {
              handleAddObject(data);
              setShowAddModal(false);
            }}
          />
        )}

        {selectedObject && (
          <NewRepairModal
            onClose={() => setSelectedObject(null)}
            onSubmit={(data) => {
              console.log("Created request for:", selectedObject.name, data);
              setSelectedObject(null);
            }}
            defaultRepairObject={selectedObject}
          />
        )}
      </div>
    </div>
  );
};

export default MyRepairObjectsPage;
