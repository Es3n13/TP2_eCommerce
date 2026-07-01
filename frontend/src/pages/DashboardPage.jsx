import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api';

const DashboardPage = () => {
  const navigate = useNavigate();
  const [declarations, setDeclarations] = useState([]);

  useEffect(() => {
    // On récupère les déclarations de l'utilisateur connecté via l'API
    api.get('/declaration/my-declarations')
      .then(res => setDeclarations(res.data))
      .catch(err => console.error("Erreur chargement dashboard", err));
  }, []);

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-3xl font-bold text-[#003366]">Mon Espace Personnel</h2>
        <button onClick={() => navigate('/declare')} className="bg-[#003366] text-white px-4 py-2 rounded-lg font-bold">
          + Nouvelle Déclaration
        </button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {declarations.length > 0 ? (
          declarations.map(decl => (
            <div key={decl.id} className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 flex flex-col justify-between">
              <div>
                <h3 className="text-xl font-bold mb-2">Année {decl.year}</h3>
                <span className={`px-2 py-1 rounded-full text-xs font-bold ${decl.status === 'Brouillon' ? 'bg-orange-100 text-orange-800' : 'bg-green-100 text-green-800'}`}>
                  {decl.status}
                </span>
              </div>
              <button 
                onClick={() => decl.status === 'Brouillon' ? navigate('/declare') : null}
                className="mt-4 w-full py-2 border border-[#003366] text-[#003366] rounded font-medium hover:bg-[#003366] hover:text-white transition-all"
              >
                {decl.status === 'Brouillon' ? 'Reprendre' : 'Télécharger'}
              </button>
            </div>
          ))
        ) : (
          <p className="text-gray-500 italic">Aucune déclaration trouvée.</p>
        )}
      </div>
    </div>
  );
};

export default DashboardPage;