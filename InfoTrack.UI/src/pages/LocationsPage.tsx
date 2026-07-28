import { useEffect, useState } from 'react';
import { axiosClient } from '../clients/axiosClient';
import { useNavigate } from 'react-router-dom';

import type ResultsResponse from '../types/ResultsResponse';
import type Location from '../types/Location';

import '../styles/pages/Data.css';

export default function LocationsPage() {

  const [locations, setLocations] = useState<Location[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchLocations = async () => {
      setIsLoading(true);
      setErrorMessage(null);

      try {
        const response = await axiosClient.get<ResultsResponse<Location>>('/locations');
        setLocations(response.data.results);
      } catch (error) {
        setErrorMessage('Unable to load locations. Please try again.');
      } finally {
        setIsLoading(false);
      }
    };
      fetchLocations();
  }, []);

    return (
      <div className="page">
        <div className="page-header">
            <h1>Solicitor Locations</h1>
        </div>

        {isLoading && <p className="loading">Loading locations...</p>}
        {errorMessage && <p className="error">{errorMessage}</p>}

        {!isLoading && !errorMessage && (
          <table className="table">
            <thead>
              <tr>
                <th className="th">Location</th>
                <th className="th">Actions</th>
              </tr>
            </thead>
            <tbody>
              {locations.length === 0 && (
                <tr>
                  <td className="table-empty" colSpan={1}>
                      No locations found.
                  </td>
                </tr>
              )}
              {locations.map((location, index) => (
                <tr key={index}>
                  <td className="td">{location.name}</td>
                  <td className="td"> <button type="button" className="button button-blue" onClick={() => navigate(`/solicitors/${location.name}`)}>View</button></td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    );
}