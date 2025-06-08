import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import Sidebar from "../components/Sidebar";
import "./styles/ProfilePage.css";
import { useAuth } from "../contexts/AuthContext";
import { useUserApi } from "../api/user";

const formatDate = (iso) => {
  const date = new Date(iso);
  return date.toLocaleDateString("en-GB");
};

const ProfilePage = () => {
  const { user } = useAuth();
  const { getUserProfile } = useUserApi();
  const navigate = useNavigate();
  const location = useLocation();

  const [profile, setProfile] = useState(null);
  const [loading, setLoading] = useState(true);
  const [message, setMessage] = useState("");

  useEffect(() => {
    if (location.state?.message) {
      setMessage(location.state.message);
      setTimeout(() => setMessage(""), 3000);
    }

    const loadProfile = async () => {
      try {
        const data = await getUserProfile();
        setProfile(data);
      } catch (err) {
        console.error("Failed to load profile:", err);
      } finally {
        setLoading(false);
      }
    };

    loadProfile();
  }, []);

  return (
    <div className="profile-container">
      <Sidebar />
      <div className="profile-page">
        <h1 className="profile-title">Profile</h1>

        {message && (
          <div className="success-banner">
            {message}
          </div>
        )}

        {(loading || !profile) ? (
          <div className="profile-card message-card">
            <p className="message-text">
              {loading ? "Loading profile..." : "Could not load user data."}
            </p>
          </div>
        ) : (
          <div className="profile-card">
            <div className="profile-columns">
              <div className="profile-left">
                <div className="section">
                  <h3>Account details:</h3>
                  <p><strong>Name:</strong> {profile.firstName}</p>
                  <p><strong>Surname:</strong> {profile.lastName}</p>
                  <p><strong>E-mail:</strong> {profile.email}</p>
                  <p><strong>Phone number:</strong> {profile.phoneNumber}</p>
                </div>

                <div className="section">
                  <h3>Address:</h3>
                  <p><strong>Country:</strong> {profile.address.country}</p>
                  <p><strong>City:</strong> {profile.address.city}</p>
                  <p><strong>Postal code:</strong> {profile.address.postalCode}</p>
                  <p><strong>Street:</strong> {profile.address.street}</p>
                  <p><strong>House number:</strong> {profile.address.houseNumber}</p>
                  <p><strong>Apartment number:</strong> {profile.address.apartNumber}</p>
                </div>
              </div>

              <div className="profile-right">
                <div className="section">
                  <p><strong>Role:</strong> {user?.role}</p>
                  <p><strong>Account created:</strong> {formatDate(profile.accountCreated)}</p>
                  <p><strong>Last login:</strong> {formatDate(profile.lastLoginAt)}</p>
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
        )}
      </div>
    </div>
  );
};

export default ProfilePage;
