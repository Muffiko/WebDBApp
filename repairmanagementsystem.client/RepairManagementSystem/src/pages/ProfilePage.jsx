import React from "react";
import { useNavigate } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import "./styles/ProfilePage.css";

const menuItems = [
    { path: "/objects", label: "My repair objects", icon: "🧰" },
    { path: "/requests", label: "My requests", icon: "📋" },
    { path: "/profile", label: "Profile", icon: "👤" }
  ];

const mockUser = {
  name: "Jan",
  surname: "Kowalski",
  email: "jankowalski@email.com",
  phone: "123 123 123",
  role: "Customer",
  created: "01/01/2025",
  lastLogin: "05/01/2025",
  address: {
    country: "Poland",
    city: "Gliwice",
    postalCode: "44–100",
    street: "3 Maja",
    houseNumber: "13",
    apartmentNumber: "Not specified"
  }
};

const ProfilePage = () => {
  const navigate = useNavigate();
  const { name, surname, email, phone, role, created, lastLogin, address } = mockUser;

  return (
    <div className="profile-container">
      <Sidebar menuItems={menuItems} />
      <div className="profile-page">
        <h1 className="profile-title">Profile</h1>

        <div className="profile-card">
          <div className="profile-columns">
            <div className="profile-left">
              <div className="section">
                <h3>Account details:</h3>
                <p><strong>Name:</strong> {name}</p>
                <p><strong>Surname:</strong> {surname}</p>
                <p><strong>E-mail:</strong> {email}</p>
                <p><strong>Phone number:</strong> {phone}</p>
              </div>

              <div className="section">
                <h3>Address:</h3>
                <p><strong>Country:</strong> {address.country}</p>
                <p><strong>City:</strong> {address.city}</p>
                <p><strong>Postal code:</strong> {address.postalCode}</p>
                <p><strong>Street:</strong> {address.street}</p>
                <p><strong>House number:</strong> {address.houseNumber}</p>
                <p><strong>Apartment number:</strong> {address.apartmentNumber}</p>
              </div>
            </div>

            <div className="profile-right">
              <div className="section">
                <p><strong>Role:</strong> {role}</p>
                <p><strong>Account created:</strong> {created}</p>
                <p><strong>Last login:</strong> {lastLogin}</p>
              </div>

              <div className="profile-buttons">
                <button onClick={() => navigate("/profile/change-password")}>
                  Change password
                </button>
                <button onClick={() => navigate("/profile/edit-info")}>
                  Edit personal info
                </button>
                <button onClick={() => navigate("/profile/change-address")}>
                  Change address
                </button>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProfilePage;
