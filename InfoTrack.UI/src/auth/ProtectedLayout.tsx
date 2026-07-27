// ProtectedLayout.tsx
import { Outlet } from "react-router-dom";

import { Nav } from "../components/global/Nav";
import { Footer } from "../components/global/Footer";

import ProtectedRoute from "./ProtectedRoute";

export const ProtectedLayout = () => (
  <ProtectedRoute>
    <Nav />
    <main className="main-container">
      <Outlet />
    </main>
    <Footer />
  </ProtectedRoute>
);