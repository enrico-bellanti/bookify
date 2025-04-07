import { useEffect, useState } from "react";
import { useAuth } from "../../shared/components/auth/AuthContext";

export default function Profile() {
  const auth = useAuth();
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string>('');

  useEffect(() => {
    // Simuliamo un caricamento iniziale breve
    const timer = setTimeout(() => {
      setLoading(false);
    }, 500);

    return () => clearTimeout(timer);
  }, []);

  if (loading) return <div>Caricamento profilo...</div>;
  if (error) return <div className="text-red-500">{error}</div>;
  if (!auth.isAuthenticated || !auth.user) return <div>Non autenticato</div>;

  return (
    <div className="bg-white shadow-md rounded p-6">
      <h1 className="text-2xl font-bold mb-4">Il tuo profilo</h1>
      
      <div className="space-y-4">
        <div>
          <h2 className="text-lg font-semibold">Info utente</h2>
          <p>Username: {auth.user.profile.preferred_username}</p>
          <p>Email: {auth.user.profile.email}</p>
          <p>Nome: {auth.user.profile.given_name} {auth.user.profile.family_name}</p>
        </div>
        
        <div>
          <h2 className="text-lg font-semibold">Ruoli</h2>
          <ul className="list-disc pl-5">
            {auth.user.profile.realm_access?.roles.map((role: string) => (
              <li key={role}>{role}</li>
            ))}
          </ul>
        </div>

        <div className="mt-4">
          <h2 className="text-lg font-semibold">Ruoli specifici per client</h2>
          {auth.user.profile.resource_access && (
            <div>
              {Object.entries(auth.user.profile.resource_access).map(([clientId, access]) => (
                <div key={clientId} className="mb-2">
                  <h3 className="font-medium">{clientId}:</h3>
                  <ul className="list-disc pl-5">
                    {access.roles.map((role: string) => (
                      <li key={`${clientId}-${role}`}>{role}</li>
                    ))}
                  </ul>
                </div>
              ))}
            </div>
          )}
        </div>

        <div className="mt-4">
          <button
            onClick={() => auth.logout()}
            className="bg-red-500 hover:bg-red-600 text-white font-bold py-2 px-4 rounded"
          >
            Logout
          </button>
        </div>
      </div>
    </div>
  );
}