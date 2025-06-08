import React, { Suspense, lazy } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { useAuth } from "./contexts/AuthContext";
import LoadingScreen from "./components/LoadingScreen";

// Lazy-loaded pages
const LoginPage = lazy(() => import("./pages/LoginPage"));
const RegisterPage = lazy(() => import("./pages/RegisterPage"));
const RequestsPage = lazy(() => import("./pages/RequestsPage"));
const RequestDetailsPage = lazy(() => import("./pages/RequestDetailsPage"));
const ProfilePage = lazy(() => import("./pages/ProfilePage"));
const ChangePasswordPage = lazy(() => import("./pages/ChangePasswordPage"));
const EditPersonalInfoPage = lazy(() => import("./pages/EditPersonalInfoPage"));
const ChangeAddressPage = lazy(() => import("./pages/ChangeAddressPage"));
const MyRepairObjectsPage = lazy(() => import("./pages/MyRepairObjectsPage"));
const NewRequestsPage = lazy(() => import("./pages/NewRequestsPage"));
const OpenRequestsPage = lazy(() => import("./pages/OpenRequestsPage"));
const WorkersPage = lazy(() => import("./pages/WorkersPage"));
const TasksPage = lazy(() => import("./pages/TasksPage"));
const RequestDetailsPage = lazy(() => import("./pages/RequestDetailsPage"));

function App() {
  const { isAuthenticated, isAuthReady } = useAuth();

  if (!isAuthReady) return <LoadingScreen />;

  return (
    <Suspense fallback={<LoadingScreen />}>
      <Routes>
        <Route path="/" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {isAuthenticated && (
          <>
            <Route path="/requests" element={<RequestsPage />} />
            <Route path="/requests/:id" element={<RequestDetailsPage />} />
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/profile/change-password" element={<ChangePasswordPage />} />
            <Route path="/profile/edit-info" element={<EditPersonalInfoPage />} />
            <Route path="/profile/change-address" element={<ChangeAddressPage />} />
            <Route path="/objects" element={<MyRepairObjectsPage />} />
            <Route path="/new-requests" element={<NewRequestsPage />} />
            <Route path="/open-requests" element={<OpenRequestsPage />} />
            <Route path="/workers" element={<WorkersPage />} />
            <Route path="/tasks" element={<TasksPage />} />
            <Route path="/repairs/:id" element={<RequestDetailsPage />} />
          </>
        )}

        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Suspense>
  );
}

export default App;
