import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import api from '../services/api'; // Import de l'api

const DashboardPage = () => {
  const navigate = useNavigate();
  const [declarations, setDeclarations] = useState([]);
  const [isLoading, setIsLoading] = useState(true);
  useEffect(() => {
    const fetchDeclarations = async () => {
      try {
        const data = await api.get('/TaxDeclarations/me'); 
        const formatted = data.map(decl => ({
          id: decl.id,
          year: decl.taxYear,
          status: decl.status === 'Draft' ? 'Brouillon' : 'Terminée',
          color: decl.status === 'Draft' ? 'bg-orange-100 text-orange-800' : 'bg-green-100 text-green-800',
          action: decl.status === 'Draft' ? 'Reprendre' : 'Télécharger'
        }));
        
        setDeclarations(formatted);
      } catch (err) {
        console.error("Erreur lors du chargement des déclarations", err);
      } finally {
        setIsLoading(false);
      }
    };

    fetchDeclarations();
  }, []);

  if (isLoading) return <div className="p-8 text-center">Chargement de vos documents...</div>;

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-3xl font-bold text-[#003366]">Mon Espace Personnel</h2>
        <button 
          onClick={() => navigate('/declare')}
          className="bg-[#003366] text-white px-4 py-2 rounded-lg font-bold hover:bg-blue-900"
        >
          + Nouvelle Déclaration
        </button>
      </div>

      <div className="grid grid-cols-1 md:grid-cols-3 gap-6">
        {declarations.length > 0 ? (
          declarations.map(decl => (
            <div key={decl.id} className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 flex flex-col justify-between">
              <div>
                <h3 className="text-xl font-bold mb-2">Année {decl.year}</h3>
                <span className={`px-2 py-1 rounded-full text-xs font-bold ${decl.color}`}>
                  {decl.status}
                </span>
              </div>
              <button 
                // 🚀 On passe l'ID dans l'URL pour que le formulaire sache quoi charger
                onClick={() => decl.status === 'Brouillon' ? navigate(`/declare?id=${decl.id}`) : null}
                className="mt-4 w-full py-2 border border-[#003366] text-[#003366] rounded font-medium hover:bg-[#003366] hover:text-white transition-all"
              >
                {decl.action}
              </button>
            </div>
          ))
        ) : (
          <p className="text-gray-500 col-span-3 text-center">Aucune déclaration trouvée.</p>
        )}
      </div>
    </div>
  );
};

export default DashboardPage;