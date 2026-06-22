import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const DeclarationFormPage = () => {
  const [step, setStep] = useState(1);
  const navigate = useNavigate();

  const steps = ['Identité', 'Revenus', 'Justificatifs', 'Récapitulatif'];

  return (
    <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-xl overflow-hidden border border-gray-200">
      {/* ProgressBar */}
      <div className="bg-gray-100 p-4 flex justify-between border-b">
        {steps.map((s, i) => (
          <div key={s} className="flex items-center gap-2">
            <div className={`w-8 h-8 rounded-full flex items-center justify-center font-bold ${step > i + 1 ? 'bg-green-500 text-white' : step === i + 1 ? 'bg-[#003366] text-white' : 'bg-gray-300 text-gray-600'}`}>
              {i + 1}
            </div>
            <span className={`text-sm font-medium ${step === i + 1 ? 'text-[#003366] font-bold' : 'text-gray-500'}`}>{s}</span>
          </div>
        ))}
      </div>

      <div className="p-8">
        {step === 1 && (
          <div className="space-y-4">
            <h3 className="text-2xl font-bold mb-4">Informations d'Identité</h3>
            <div className="grid grid-cols-2 gap-4">
              <div className="flex flex-col gap-1">
                <label className="text-sm font-medium">Prénom</label>
                <input type="text" className="p-2 border rounded" placeholder="Jean" />
              </div>
              <div className="flex flex-col gap-1">
                <label className="text-sm font-medium">Nom</label>
                <input type="text" className="p-2 border rounded" placeholder="Tremblay" />
              </div>
            </div>
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium">Adresse Résidentielle</label>
              <input type="text" className="p-2 border rounded" placeholder="123 Rue de la Montagne, Montréal" />
            </div>
          </div>
        )}

        {step === 2 && (
          <div className="space-y-4">
            <h3 className="text-2xl font-bold mb-4">Revenus Annuels</h3>
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium">Montant Total des Revenus ($)</label>
              <input type="number" className="p-2 border rounded text-lg font-bold" placeholder="0.00" />
              <p className="text-xs text-gray-500 mt-1 italic">Note: Ce système est réservé aux revenus inférieurs à 30 000 $.</p>
            </div>
            <div className="flex items-center gap-2 mt-4">
              <input type="checkbox" id="citizen" />
              <label htmlFor="citizen" className="text-sm">Je confirme être citoyen canadien ou résident permanent.</label>
            </div>
          </div>
        )}

        {step === 3 && (
          <div className="space-y-4">
            <h3 className="text-2xl font-bold mb-4">Justificatifs</h3>
            <div className="border-2 border-dashed border-gray-300 rounded-lg p-12 text-center hover:border-[#003366] transition-colors cursor-pointer">
              <p className="text-gray-500">Glissez-déposez vos documents ici ou <span className="text-[#003366] font-bold underline">parcourez vos fichiers</span></p>
              <p className="text-xs text-gray-400 mt-2">Formats acceptés: PDF, JPG, PNG (max 5MB)</p>
            </div>
            <div className="flex gap-2">
              <div className="flex items-center gap-2 p-2 bg-gray-50 border rounded text-sm">
                📄 T4_2024.pdf <button className="text-red-500 font-bold">×</button>
              </div>
            </div>
          </div>
        )}

        {step === 4 && (
          <div className="space-y-6">
            <h3 className="text-2xl font-bold mb-4">Récapitulatif Final</h3>
            <div className="bg-gray-50 p-4 rounded-lg border space-y-2">
              <div className="flex justify-between border-b py-1"><span>Nom:</span> <span className="font-medium">Jean Tremblay</span></div>
              <div className="flex justify-between border-b py-1"><span>Revenu déclaré:</span> <span className="font-medium">24 500 $</span></div>
              <div className="flex justify-between border-b py-1"><span>Documents:</span> <span className="font-medium">1 fichier joint</span></div>
            </div>
            <div className="p-4 bg-blue-50 border-l-4 border-blue-500 text-sm text-blue-700">
              En soumettant ce formulaire, vous certifiez que les informations fournies sont exactes.
            </div>
          </div>
        )}

        <div className="mt-8 flex justify-between">
          <button 
            onClick={() => step > 1 ? setStep(step - 1) : navigate('/dashboard')}
            className={`px-6 py-2 rounded font-medium ${step === 1 ? 'text-gray-400 cursor-not-allowed' : 'text-gray-600 hover:bg-gray-100'}`}
            disabled={step === 1}
          >
            Précédent
          </button>
          {step < 4 ? (
            <button 
              onClick={() => setStep(step + 1)}
              className="bg-[#003366] text-white px-6 py-2 rounded font-bold hover:bg-blue-900"
            >
              Suivant
            </button>
          ) : (
            <button 
              onClick={() => { alert('Déclaration soumise !'); navigate('/dashboard'); }}
              className="bg-green-600 text-white px-6 py-2 rounded font-bold hover:bg-green-700"
            >
              Soumettre Officiellement
            </button>
          )}
        </div>
      </div>
    </div>
  );
};

export default DeclarationFormPage;
