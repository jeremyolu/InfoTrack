import { useNavigate } from 'react-router-dom';
import Logo from "../assets/infotrack_logo.png";

import '../styles/pages/Dashboard.css';

export default function DashboardPage() {

  const navigate = useNavigate();

  return (
    <div className="dashboard-page-container">
      <div className="logo-container">
        <img src={Logo} alt="InfoTrack logo" />
      </div>

      <div className="dashboard-page-inner">
        <div className="dashboard-item">
          <div className="dashboard-item-content">
            <h2 className="dashboard-item-title">Locations</h2>
            <p className="dashboard-item-text">View a list of configured locations</p>
             <button type="button" className="button button-white" onClick={() =>navigate('/locations')}>
                View Locations
            </button>
          </div>
        </div>
        <div className="dashboard-item">
          <div className="dashboard-item-content">
            <h2 className="dashboard-item-title">Solicitors</h2>
            <p className="dashboard-item-text">View a list of specified solicitors</p>
            <button type="button" className="button button-white" onClick={() => navigate(`/solicitors/london`)}>
                View Solicitors
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}