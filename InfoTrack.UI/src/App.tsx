// App.tsx
import { BrowserRouter, Routes, Route } from "react-router-dom";

import { AuthProvider } from "./contexts/AuthContext";
import { ProtectedLayout } from "./auth/ProtectedLayout";

import LoginPage from "./pages/LoginPage";
import DashboardPage from "./pages/DashboardPage";
import SolicitorsPage from "./pages/SolicitorsPage";
import LocationsPage from "./pages/LocationsPage";
import SolicitorDataPage from "./pages/SolicitorDataPage";

import './styles/components/App.css';

export default function App() {
  return (
    <BrowserRouter>
      <AuthProvider>
        <Routes>
          <Route path="/login" element={<LoginPage />} />
          <Route element={<ProtectedLayout />}>
            <Route path="/" element={<DashboardPage />} />
            <Route path="/locations" element={<LocationsPage />} />
            <Route path="/solicitors/:location" element={<SolicitorsPage />} />
            <Route path="/solicitors" element={<SolicitorDataPage />} />
          </Route>
        </Routes>
      </AuthProvider>
    </BrowserRouter>
  );
}