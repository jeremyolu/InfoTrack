// App.tsx
import { BrowserRouter, Routes, Route } from "react-router-dom";

import { Nav } from "./components/global/Nav";
import { Footer } from "./components/global/Footer";

import { DashboardPage } from "./pages/DashboardPage";
// import { LocationsPage } from "./pages/LocationsPage";
// import { SolicitorsPage } from "./pages/SolicitorsPage";

import './styles/components/App.css';


export default function App() {
  return (
    <BrowserRouter>
    <Nav />
      <main className="main-container">
        <Routes>
          <Route path="/" element={<DashboardPage />} />
          {/* <Route path="/locations" element={<LocationsPage />} />
          <Route path="/solicitors" element={<SolicitorsPage />} /> */}
        </Routes>
      </main>
      <Footer />
    </BrowserRouter>
  );
}