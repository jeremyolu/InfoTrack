import { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import { axiosClient } from '../clients/axiosClient';

import type ResultsResponse from '../types/ResultsResponse';
import type Solicitor from '../types/Solicitor';
import type Location from '../types/Location';

import '../styles/pages/Data.css';

export default function SolicitorsPage() {

  const { location } = useParams<{ location: string }>();

  const [solicitors, setSolicitors] = useState<Solicitor[]>([]);
  const [locations, setLocations] = useState<Location[]>([]);
  const [isLoading, setIsLoading] = useState(true);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchLocations = async () => {
      try {
        const response = await axiosClient.get<ResultsResponse<Location>>('/locations');
        setLocations(response.data.results);
      } catch (error) {
        console.log(error);
      }
    };
    fetchLocations();
  }, []);

  useEffect(() => {
    const fetchSolicitors = async () => {
      setIsLoading(true);
      setErrorMessage(null);

      try {
        const response = await axiosClient.get<ResultsResponse<Solicitor>>('/solicitors', {
          params: {
            location: location,
            sortBy: 'name_asc',
          },
        });

        setSolicitors(response.data.results);
      } catch (error) {
        console.log(error);
        setErrorMessage('Unable to load solicitors. Please try again.');
      } finally {
        setIsLoading(false);
      }
    };
      fetchSolicitors();
  }, [location]);

  const handleLocationChange = (event: any) => {
    const newLocation = event.target.value;
    navigate(`/solicitors/${newLocation.toLowerCase()}`);
  };

  const handleView = (website: string) => {
   // TODO: navigate to solictor dedicated view
  };

  return (
    <div className="page">
      <div className="page-header">
        <h1>Solicitors in {location}</h1>

        <select className="select" value={location} onChange={handleLocationChange}>
            {locations.map((loc) => (
              <option key={loc.name} value={loc.name.toLowerCase()}>
                  {loc.name}
              </option>
            ))}
        </select>
      </div>

      {isLoading && <p className="loading">Loading solicitors...</p>}
      {errorMessage && <p className="error">{errorMessage}</p>}

      {!isLoading && !errorMessage && (
        <table className="table">
          <thead>
            <tr>
              <th className="th">Name</th>
              <th className="th">Address</th>
              <th className="th">Telephone</th>
              <th className="th">Actions</th>
            </tr>
          </thead>
          <tbody>
            {solicitors.length === 0 && (
              <tr>
                <td className="table-empty" colSpan={4}>
                    No solicitors found.
                </td>
              </tr>
            )}
            {solicitors.map((solicitor, index) => (
              <tr key={index}>
                <td className="td">{solicitor.name}</td>
                <td className="td">
                  {solicitor.address.addressLine1}
                  {solicitor.address.postcode ? `, ${solicitor.address.postcode}` : ''}
                </td>
                <td className="td">{solicitor.contactDetails.telephone}</td>
                <td className="td">
                  <button
                    type="button"
                    className="button button-blue"
                    onClick={() => handleView(solicitor.contactDetails.website)}>Visit
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
}