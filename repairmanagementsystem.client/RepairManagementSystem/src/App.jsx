import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import LoginPage from "./pages/LoginPage";
import RegisterPage from "./pages/RegisterPage";
import RequestsPage from "./pages/RequestsPage";
import RepairDetailsPage from "./pages/RepairDetailsPage";
import ProfilePage from "./pages/ProfilePage";
import ChangePasswordPage from "./pages/ChangePasswordPage";
import EditPersonalInfoPage from "./pages/EditPersonalInfoPage";
import ChangeAddressPage from "./pages/ChangeAddressPage";
import MyRepairObjectsPage from "./pages/MyRepairObjectsPage";
import NewRequestsPage from "./pages/NewRequestsPage";

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />
        <Route path="/requests" element={<RequestsPage />} />
        <Route path="/requests/:id" element={<RepairDetailsPage />} />
        <Route path="/profile" element={<ProfilePage />} />
        <Route path="/profile/change-password" element={<ChangePasswordPage />} />
        <Route path="/profile/edit-info" element={<EditPersonalInfoPage />} />
        <Route path="/profile/change-address" element={<ChangeAddressPage />} />
        <Route path="/objects" element={<MyRepairObjectsPage />} />
        <Route path="/new-requests" element={<NewRequestsPage />} />
      </Routes>
    </Router>
  );
}

export default App;
