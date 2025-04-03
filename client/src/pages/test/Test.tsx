import axios from 'axios';
import { useState } from 'react';
import { createApiClient } from '../../open-api/api-client';

// interface TestComponentProps {
//   basePath?: string;
// }

export function Test() {
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const [response, setResponse] = useState<string | null>(null);
  
  // Utilizzo di createApiClient per ottenere l'istanza di TestApi
  const apiClient = createApiClient();
  
  const handleTestCall = async () => {
    setLoading(true);
    setError(null);
    setResponse(null);
    
    try {
      // Chiamata API utilizzando l'istanza test dal client API
      const result = await apiClient.test.apiTestGet();
      
      // Gestione della risposta - poiché result.data è void, mostriamo altre informazioni utili
      const responseInfo = {
        status: result.status,
        statusText: result.statusText,
        headers: result.headers,
        // Non includiamo result.data perché è void
      };
      setResponse(JSON.stringify(responseInfo, null, 2));
    } catch (err) {
      if (axios.isAxiosError(err)) {
        setError(`API Error: ${err.message} (${err.response?.status || 'unknown status'})`);
      } else {
        setError(`Unexpected error: ${(err as Error).message}`);
      }
      console.error('API Call failed:', err);
    } finally {
      setLoading(false);
    }
  };
  
  return (
    <div className="test-component">
      <h2>API Test</h2>
      
      <button 
        onClick={handleTestCall}
        disabled={loading}
      >
        {loading ? 'Chiamata in corso...' : 'Chiama API Test'}
      </button>
      
      {loading && <p className="loading">Caricamento...</p>}
      
      {error && (
        <div className="error">
          <h3>Errore:</h3>
          <p>{error}</p>
        </div>
      )}
      
      {response && (
        <div className="response">
          <h3>Risposta:</h3>
          <pre>{response}</pre>
        </div>
      )}
    </div>
  );
};

export default Test;