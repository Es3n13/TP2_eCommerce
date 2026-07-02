import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useDeclaration } from '../viewmodels/useDeclaration';

const DeclarationFormPage = () => {
 const navigate = useNavigate();
 const {
 step,
 setStep,
 formData,
 setFormData,
 updateField,
 documents,
 addDocument,
 submitDeclaration,
 saveDraft,
 isLoading,
 error
 } = useDeclaration();

 // 🔍 AJOUTE CETTE LIGNE ICI :
 console.log("DEBUG FORMULAIRE -> Step:", step, "FormData:", formData, "Documents:", documents);

 const steps = ['Identité', 'Revenus', 'Justificatifs', 'Récapitulatif'];

  return (
    <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-xl overflow-hidden border border-gray-200">
      {/* ProgressBar (identique à ton code) */}
      <div className="bg-gray-100 p-4 flex justify-between border-b">
        {steps.map((s, i) => (
          <div key={s} className="flex items-center gap-2">
            <div className={`w-8 h-8 rounded-full flex items-center justify-center font-bold ${step > i + 1 ? 'bg-green-500 text-white' : step === i + 1 ? 'bg-[#003366] text-white' : 'bg-gray-300 text-gray-600'}`}>{i + 1}</div>
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
                <input 
                  type="text" 
                  className="p-2 border rounded" 
                  value={formData.firstName} 
                  onChange={(e) => updateField('firstName', e.target.value)} 
                />
              </div>
              <div className="flex flex-col gap-1">
                <label className="text-sm font-medium">Nom</label>
                <input 
                  type="text" 
                  className="p-2 border rounded" 
                  value={formData.lastName} 
                  onChange={(e) => updateField('lastName', e.target.value)} 
                />
              </div>
            </div>
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium">Adresse Résidentielle</label>
              <input 
                type="text" 
                className="p-2 border rounded" 
                value={formData.address} 
                onChange={(e) => updateField('address', e.target.value)} 
              />
            </div>
          </div>
        )}
        {step === 2 && (
          <div className="space-y-4">
            <h3 className="text-2xl font-bold mb-4">Revenus Annuels</h3>
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium">Montant Total des Revenus ($)</label>
              <input 
                type="number" 
                className="p-2 border rounded text-lg font-bold" 
                value={formData.income} 
                onChange={(e) => updateField('income', e.target.value)} 
              />
            </div>
            <div className="flex items-center gap-2 mt-4">
              <input 
                type="checkbox" 
                checked={formData.isCitizen} 
                onChange={(e) => updateField('isCitizen', e.target.checked)} 
              />
              <label className="text-sm">Je confirme être citoyen canadien ou résident permanent.</label>
            </div>
          </div>
        )}

        {step === 3 && (
          <div className="space-y-4">
            <h3 className="text-2xl font-bold mb-4">Justificatifs</h3>
            <input 
              type="file" 
              className="hidden" 
              id="fileUpload" 
              onChange={(e) => addDocument(e.target.files[0])} 
            />
            <label htmlFor="fileUpload" className="block border-2 border-dashed border-gray-300 rounded-lg p-12 text-center hover:border-[#003366] cursor-pointer">
              <p className="text-gray-500">Cliquez pour uploader vos documents</p>
            </label>
            <div className="flex gap-2">
              {documents.map((doc, i) => (
                <div key={i} className="flex items-center gap-2 p-2 bg-gray-50 border rounded text-sm">
                  📄 {doc.name}
                </div>
              ))}
            </div>
          </div>
        )}
        {step === 4 && (
        <div className="space-y-6">
        <h3 className="text-2xl font-bold mb-4">Récapitulatif Final</h3>
        <div className="bg-gray-50 p-4 rounded-lg border space-y-2">
        <div className="flex justify-between border-b py-1">
          <span>Nom:</span> 
          <span className="font-medium">{formData?.firstName || 'Non renseigné'} {formData?.lastName || ''}</span>
        </div>
        <div className="flex justify-between border-b py-1">
          <span>Revenu déclaré:</span> 
          <span className="font-medium">{formData?.income || '0'} $</span>
        </div>
        <div className="flex justify-between border-b py-1">
          <span>Documents:</span> 
          <span className="font-medium">{documents?.length || 0} fichier(s) joint(s)</span>
        </div>
        </div>
        </div>
        )}

        <div className="mt-8 flex justify-between items-center">
          <button 
            onClick={() => step > 1 ? setStep(step - 1) : navigate('/dashboard')} 
            className="px-6 py-2 rounded font-medium text-gray-600 hover:bg-gray-100 transition-colors" 
            disabled={step === 1}
          >
            Précédent
          </button>
          <div className="flex gap-3">
            <button 
              onClick={async () => {
                const res = await saveDraft();
                if(res.success) {
                  alert('Brouillon sauvegardé avec succès !');
                } else {
                  alert('Erreur lors de la sauvegarde : ' + res.error);
                }
              }}
              className="px-6 py-2 rounded font-medium border border-gray-300 text-gray-600 hover:bg-gray-50 transition-colors"
              disabled={isLoading}
            >
              {isLoading ? '...' : 'Sauvegarder le brouillon'}
            </button>

            {step < 4 ? (
              <button 
                onClick={() => setStep(step + 1)} 
                className="bg-[#003366] text-white px-6 py-2 rounded font-bold hover:bg-[#002244] transition-colors"
              >
                Suivant
              </button>
            ) : (
              <button 
                onClick={async () => {
                  const res = await submitDeclaration();
                  if(res.success) {
                    alert('Déclaration soumise avec succès !');
                    navigate('/dashboard');
                  } else {
                    alert('Erreur : ' + res.error);
                  }
                }} 
                className="bg-green-600 text-white px-6 py-2 rounded font-bold hover:bg-green-700 transition-colors"
                disabled={isLoading}
              >
                {isLoading ? 'Envoi...' : 'Soumettre Officiellement'}
              </button>
            )}
          </div>
        </div>
      </div>
    </div>
  );
};
export default DeclarationFormPage;
