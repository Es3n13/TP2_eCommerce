import React from 'react';
import { useNavigate } from 'react-router-dom';

const AgentReviewPage = () => {
  const navigate = useNavigate();

  return (
    <div className="space-y-6">
      <div className="flex justify-between items-center">
        <h2 className="text-3xl font-bold text-[#003366]">Revue du Dossier #45821</h2>
        <button 
          onClick={() => navigate('/agent/queue')}
          className="text-gray-600 hover:underline"
        >
          ← Retour à la file
        </button>
      </div>

      <div className="grid grid-cols-1 lg:grid-cols-2 gap-6">
        {/* Left: User Data */}
        <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
          <h3 className="text-lg font-bold mb-4 border-b pb-2">Saisie du Contribuable</h3>
          <div className="space-y-3">
            <div className="flex justify-between"><span>Nom:</span> <span className="font-medium">Jean Tremblay</span></div>
            <div className="flex justify-between"><span>NAS:</span> <span className="font-medium">*** *** 123</span></div>
            <div className="flex justify-between"><span>Revenu Déclaré:</span> <span className="font-bold text-lg">24 500 $</span></div>
            <div className="flex justify-between"><span>Documents:</span> <span className="text-blue-600 underline cursor-pointer">T4_2024.pdf</span></div>
          </div>
        </div>

        {/* Right: Canada API Data */}
        <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200">
          <h3 className="text-lg font-bold mb-4 border-b pb-2 text-blue-800">Données Revenu Canada (API)</h3>
          <div className="space-y-3">
            <div className="flex justify-between"><span>Statut:</span> <span className="text-green-600 font-bold">Vérifié</span></div>
            <div className="flex justify-between"><span>Revenu Officiel:</span> <span className="font-bold text-lg text-red-600">26 800 $</span></div>
            <div className="p-3 bg-red-50 text-red-700 text-sm rounded border border-red-200">
              <strong>Écart détecté :</strong> Différence de 2 300 $ constatée.
            </div>
          </div>
        </div>
      </div>

      {/* Decision Panel */}
      <div className="bg-white p-6 rounded-xl shadow-sm border border-gray-200 space-y-4">
        <h3 className="text-lg font-bold">Décision de l'Agent</h3>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          <div className="flex flex-col gap-1">
            <label className="text-sm font-medium">Montant Final Ajusté ($)</label>
            <input type="number" className="p-2 border rounded font-bold" defaultValue="26800" />
          </div>
          <div className="flex flex-col gap-1">
            <label className="text-sm font-medium">Note pour l'Avis de Cotisation</label>
            <textarea className="p-2 border rounded h-20" placeholder="Expliquer l'ajustement..."></textarea>
          </div>
        </div>
        <div className="flex justify-end gap-4">
          <button className="px-4 py-2 border rounded text-gray-600 hover:bg-gray-100">Rejeter le dossier</button>
          <button 
            onClick={() => { alert('Avis généré avec succès !'); navigate('/agent/queue'); }}
            className="bg-green-600 text-white px-6 py-2 rounded font-bold hover:bg-green-700"
          >
            Valider et Générer l'Avis
          </button>
        </div>
      </div>
    </div>
  );
};

export default AgentReviewPage;
