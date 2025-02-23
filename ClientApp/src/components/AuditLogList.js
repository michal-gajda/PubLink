import React, { useState, useEffect } from 'react';

const AuditLogList = () => {
  const [logs, setLogs] = useState([]);
  const [page, setPage] = useState(1);
  const [organizations, setOrganizations] = useState([]);
  const [selectedOrganizationId, setSelectedOrganizationId] = useState('');
  const [isLoading, setIsLoading] = useState(false);
  const [isLoadingOrganizations, setIsLoadingOrganizations] = useState(false);
  const [error, setError] = useState(null);

  // Pobranie listy organizacji
  const fetchOrganizations = async () => {
    setIsLoadingOrganizations(true);
    try {
      const response = await fetch('/api/organizations/');
      if (!response.ok) {
        throw new Error('Błąd pobierania listy organizacji');
      }
      const data = await response.json();
      setOrganizations(data);
      if (data.length > 0 && !selectedOrganizationId) {
        setSelectedOrganizationId(data[0]);
      }
    } catch (err) {
      console.error(err);
    }
    setIsLoadingOrganizations(false);
  };

  const fetchLogs = async () => {
    if (!selectedOrganizationId) return;

    setIsLoading(true);
    setError(null);
    try {
      const response = await fetch(
        `/api/auditlog?organizationId=${selectedOrganizationId}&page=${page}`
      );
      if (!response.ok) {
        throw new Error('Błąd pobierania danych');
      }
      const data = await response.json();
      setLogs(data);
    } catch (err) {
      setError(err.message);
    }
    setIsLoading(false);
  };

  useEffect(() => {
    fetchOrganizations();
  }, []);

  useEffect(() => {
    fetchLogs();
  }, [page, selectedOrganizationId]);

  const handleOrganizationChange = (event) => {
    setSelectedOrganizationId(event.target.value);
    setPage(1);
  };

  const handlePrevPage = () => {
    if (page > 1) {
      setPage(page - 1);
    }
  };

  const handleNextPage = () => {
    setPage(page + 1);
  };

  return (
    <div>
      <h1>Audit Log</h1>
      <div style={{ marginBottom: '20px' }}>
        <label htmlFor="organizationSelect">Wybierz organizację: </label>
        {isLoadingOrganizations ? (
          <span>Ładowanie organizacji...</span>
        ) : (
          <select
            id="organizationSelect"
            value={selectedOrganizationId}
            onChange={handleOrganizationChange}
          >
            {organizations.map((org) => (
              <option key={org} value={org}>
                {org}
              </option>
            ))}
          </select>
        )}
      </div>
      {isLoading && <p>Ładowanie danych...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {!isLoading && !error && (
        <table border="1" cellPadding="5">
          <thead>
            <tr>
              <th>User Email</th>
              <th>Type</th>
              <th>Contract Number</th>
              <th>Start At</th>
              <th>Duration</th>
              <th>Number of Entities Changed</th>
            </tr>
          </thead>
          <tbody>
            {logs.map((log, index) => (
              <tr key={index}>
                <td>{log.userEmail}</td>
                <td>{log.type}</td>
                <td>{log.contractNumber || '-'}</td>
                <td>{new Date(log.startAt).toLocaleString()}</td>
                <td>{log.duration}</td>
                <td>{log.numberOfEntitiesChanged}</td>
              </tr>
            ))}
          </tbody>
        </table>
      )}
      <div style={{ marginTop: '20px' }}>
        <button onClick={handlePrevPage} disabled={page <= 1}>
          Poprzednia strona
        </button>
        <span style={{ margin: '0 10px' }}>Strona: {page}</span>
        <button onClick={handleNextPage}>Następna strona</button>
      </div>
    </div>
  );
};

export default AuditLogList;
