import React, { Suspense, lazy } from "react";
import { Routes, Route, Navigate } from "react-router-dom";
import { useAuth } from "./contexts/AuthContext";
import LoadingScreen from "./components/LoadingScreen";
import ProtectedRoute from "./components/ProtectedRoute";
import MyTaskDetailPage from "./pages/MyTaskDetailPage";


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
const FinishedRequestsPage = lazy(() => import("./pages/FinishedRequestsPage"));
const WorkersPage = lazy(() => import("./pages/WorkersPage"));
const TasksPage = lazy(() => import("./pages/TasksPage"));
const ManageRequestPage = lazy(() => import("./pages/ManageRequestPage"));
const AdminPage = lazy(() => import("./pages/AdminPage"));

function App() {
  const { isAuthenticated, isAuthReady } = useAuth();

  if (!isAuthReady) return <LoadingScreen />;

  return (
    <Suspense fallback={<LoadingScreen />}>
      <Routes>
        {/* Public routes */}
        <Route path="/" element={<LoginPage />} />
        <Route path="/register" element={<RegisterPage />} />

        {isAuthenticated && (
          <>
            {/* Customer */}
            <Route
              path="/requests"
              element={
                <ProtectedRoute allowedRoles={["Customer"]}>
                  <RequestsPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/requests/:id"
              element={
                <ProtectedRoute allowedRoles={["Customer"]}>
                  <RequestDetailsPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/objects"
              element={
                <ProtectedRoute allowedRoles={["Customer"]}>
                  <MyRepairObjectsPage />
                </ProtectedRoute>
              }
            />

            {/* Manager */}
            <Route
              path="/new-requests"
              element={
                <ProtectedRoute allowedRoles={["Manager"]}>
                  <NewRequestsPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/open-requests"
              element={
                <ProtectedRoute allowedRoles={["Manager"]}>
                  <OpenRequestsPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/finished-requests"
              element={
                <ProtectedRoute allowedRoles={["Manager"]}>
                  <FinishedRequestsPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/workers"
              element={
                <ProtectedRoute allowedRoles={["Manager"]}>
                  <WorkersPage />
                </ProtectedRoute>
              }
            />
            <Route
              path="/manage-request/:id"
              element={
                <ProtectedRoute allowedRoles={["Manager"]}>
                  <ManageRequestPage />
                </ProtectedRoute>
              }
            />

            {/* Worker */}
            <Route
              path="/tasks"
              element={
                <ProtectedRoute allowedRoles={["Worker"]}>
                  <TasksPage />
                </ProtectedRoute>
              }
            />
              <Route path="/tasks/my/:number" element={<MyTaskDetailPage />} />

            {/* Admin */}
            <Route
              path="/admin"
              element={
                <ProtectedRoute allowedRoles={["Admin"]}>
                  <AdminPage />
                </ProtectedRoute>
              }
            />

            {/* Common: Profile */}
            <Route path="/profile" element={<ProfilePage />} />
            <Route path="/profile/change-password" element={<ChangePasswordPage />} />
            <Route path="/profile/edit-info" element={<EditPersonalInfoPage />} />
            <Route path="/profile/change-address" element={<ChangeAddressPage />} />
          </>
        )}

        {/* Fallback */}
        <Route path="*" element={<Navigate to="/" replace />} />
      </Routes>
    </Suspense>
  );
}

export default App;