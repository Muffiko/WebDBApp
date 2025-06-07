import React, { useState, useEffect } from "react";
import Sidebar from "../components/Sidebar";
import FilterBar from "../components/FilterBar";
import AddRepairObjectModal from "../components/AddRepairObjectModal";
import NewRequestModal from "../components/NewRequestModal";
import { useRepairObjectApi } from "../api/repairObjects";
import "./styles/MyRepairObjectsPage.css";

const menuItems = [
  { path: "/objects", label: "My repair objects", icon: "🧰" },
  { path: "/requests", label: "My requests", icon: "📋" },
  { path: "/profile", label: "Profile", icon: "👤" }
];

const MyRepairObjectsPage = () => {
  const { getCustomerRepairObjects } = useRepairObjectApi();

  const [objects, setObjects] = useState([]);
  const [filters, setFilters] = useState({ name: "", type: "" });
  const [showAddModal, setShowAddModal] = useState(false);
  const [selectedObject, setSelectedObject] = useState(null);

  const loadObjects = async () => {
    try {
      const data = await getCustomerRepairObjects();
      setObjects(data);
    } catch (err) {
      console.error("Failed to load repair objects", err);
    }
  };

  useEffect(() => {
    loadObjects();
  }, []);

  const filteredObjects = objects.filter((obj) =>
    obj.name.toLowerCase().includes(filters.name.toLowerCase()) &&
    (filters.type === "" || obj.repairObjectType?.name === filters.type)
  );

  const uniqueTypes = [...new Set(objects.map(obj => obj.repairObjectType?.name))];

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
              label: "All types",
              options: uniqueTypes
            }
          ]}
        />

        <div className="object-list">
          {filteredObjects.map((obj) => (
            <div key={obj.repairObjectId} className="object-card">
              <div className="object-info">
                <h3>{obj.name}</h3>
                <p>Type: {obj.repairObjectType?.name}</p>
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
            onSuccess={() => {
              setShowAddModal(false);
              loadObjects();
            }}
          />
        )}

        {selectedObject && (
          <NewRequestModal
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