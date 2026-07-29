// SolicitorPage.tsx
import { useLocation, useNavigate } from 'react-router-dom';

import type Solicitor from '../types/data/Solicitor';

import '../styles/pages/Data.css';

export default function SolicitorDataPage() {

  const location = useLocation();
  const solicitor = location.state?.solicitor as Solicitor | undefined;
  
  const navigate = useNavigate();

  if (!solicitor) return <div className="solicitor-error">Solicitor not found.</div>;

  return (
    <div className="solicitor-page">
      <div className="solicitor-header">
        <div>
          <img
            src={solicitor.logoUrl}
            alt={`${solicitor.name} logo`}
            className="solicitor-logo"/>
          <div>
            <h1 className="solicitor-name">{solicitor.name}</h1>
            <p className="solicitor-location">
            {solicitor.address.location}
            </p>
          </div>
       </div>
        <button className="button button-blue" onClick={() => navigate(-1)} >Back</button>
      </div>

      <p className="solicitor-description">{solicitor.description}</p>

      <div className="solicitor-grid">
        <div className="solicitor-card">
          <h2>Address</h2>
          <p>{solicitor.address.addressLine1}</p>
          <p>{solicitor.address.location}</p>
          <p>{solicitor.address.postcode}</p>
        </div>

        <div className="solicitor-card">
          <h2>Contact</h2>
          <p>
            <a href={`tel:${solicitor.contactDetails.telephone}`}>
              {solicitor.contactDetails.telephone}
            </a>
          </p>
          <p>
            
           <a href={solicitor.contactDetails.website}
              target="_blank"
              rel="noopener noreferrer">
              {solicitor.contactDetails.website}
            </a>
          </p>
        </div>
      </div>
    </div>
  );
}