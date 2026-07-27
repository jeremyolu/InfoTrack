import Logo from "../assets/infotrack_logo.png";

import '../styles/pages/Dashboard.css';

export function DashboardPage() {

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
             <button>View Locations</button>
          </div>
        </div>
        <div className="dashboard-item">
          <div className="dashboard-item-content">
            <h2 className="dashboard-item-title">Silictors</h2>
            <p className="dashboard-item-text">View a list of specified locations</p>
            <button>View Silictors</button>
          </div>
        </div>
      </div>
    </div>
  );
}