import React, { useEffect, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useDeclaration } from '../viewmodels/useDeclaration';

const DeclarationFormPage = () => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const id = searchParams.get('id');
  
  const [isResuming, setIsResuming] = useState(!!id);

  const {
    step, setStep, formData, updateField,
    documents, addDocument, submitDeclaration,
    saveDraft, initializeDeclaration, loadDeclaration, isLoading, error
  } = useDeclaration();

  const steps = ['Année', 'Revenus', 'Justificatifs', 'Récapitulatif'];

  // ✅ Logique de reprise améliorée
  useEffect(() => {
    if (id) {
      const resume = async () => {
        setIsResuming(true);
        const res = await loadDeclaration(id);
        if (res.success) {
          setStep(2); 
          setIsResuming(false);
        } else {
          alert("Erreur lors de la reprise : " + res.error);
          navigate('/dashboard');
        }
      };
      resume();
    }
  }, [id, navigate]);
  // ✅ Nouvelle logique du bouton "Suivant"
  const handleNext = async () => {
    if (step === 1) {
      // Si on est à l'étape 1, on initialise la déclaration avant de passer à la suite
      if (!formData.taxYear) {
        alert("Veuillez choisir une année.");
        return;
      }
      const res = await initializeDeclaration(formData.taxYear);
      if (!res.success) {
        alert("Erreur : " + res.error);
        return;
      }
    }
    setStep(step + 1);
  };

  // 🌟 Écran de chargement pour éviter le flash de l'étape 1 lors d'une reprise
  if (isResuming && isLoading) {
    return (
      <div className="flex flex-col items-center justify-center min-h-[400px] space-y-4">
        <div className="w-12 h-12 border-4 border-[#003366] border-t-transparent rounded-full animate-spin"></div>
        <p className="text-[#003366] font-medium">Chargement de votre brouillon...</p>
      </div>
    );
  }

  return (
    <div className="max-w-4xl mx-auto bg-white shadow-lg rounded-xl overflow-hidden border border-gray-200">
      {/* Progress Bar */}
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
        {/* STEP 1: ANNÉE */}
        {step === 1 && (
          <div className="space-y-6 text-center py-10">
            <h3 className="text-3xl font-bold text-[#003366]">Bienvenue</h3>
            <p className="text-gray-600">Pour commencer, veuillez indiquer l'année fiscale de votre déclaration.</p>
            <div className="flex flex-col items-center gap-2 max-w-xs mx-auto">
              <label className="text-sm font-medium text-left w-full">Année de la déclaration</label>
              <input
                type="number"
                className="p-3 border rounded-lg w-full text-center text-2xl font-bold focus:ring-2 focus:ring-blue-500 outline-none"
                value={formData.taxYear}
                onChange={(e) => updateField('taxYear', e.target.value)}
              />
            </div>
            {/* ❌ On a supprimé le bouton "Commencer" d'ici */}
            {error && <p className="text-red-500 font-medium">{error}</p>}
          </div>
        )}
        {/* STEP 2: REVENUS */}
        {step === 2 && (
          <div className="space-y-4">
            <h3 className="text-2xl font-bold mb-4">Revenus Annuels</h3>
            <div className="flex flex-col gap-1">
              <label className="text-sm font-medium">Montant Total des Revenus ($)</label>
              <input type="number" className="p-2 border rounded text-lg font-bold" value={formData.income} onChange={(e) => updateField('income', e.target.value)} />
            </div>
            <div className="flex items-center gap-2 mt-4">
              <input type="checkbox" checked={formData.isCitizen} onChange={(e) => updateField('isCitizen', e.target.checked)} />
              <label className="text-sm">Je confirme être citoyen canadien ou résident permanent.</label>
            </div>
          </div>
        )}
        {/* STEP 3: JUSTIFICATIFS */}
        {step === 3 && (
          <div className="space-y-4">
            <h3 className="text-2xl font-bold mb-4">Justificatifs</h3>
            <input type="file" className="hidden" id="fileUpload" onChange={(e) => addDocument(e.target.files[0])} />
            <label htmlFor="fileUpload" className="block border-2 border-dashed border-gray-300 rounded-lg p-12 text-center hover:border-[#003366] cursor-pointer">
              <p className="text-gray-500">Cliquez pour uploader vos documents</p>
            </label>
            <div className="flex gap-2">
              {documents.map((doc, i) => (
                <div key={i} className="flex items-center gap-2 p-2 bg-gray-50 border rounded text-sm">{doc.name}</div>
              ))}
            </div>
          </div>
        )}

        {/* STEP 4: RÉCAPITULATIF */}
        {step === 4 && (
          <div className="space-y-6">
            <h3 className="text-2xl font-bold mb-4">Récapitulatif Final</h3>
            <div className="bg-gray-50 p-4 rounded-lg border space-y-2">
              <div className="flex justify-between border-b py-1"><span>Année:</span> <span className="font-medium">{formData.taxYear}</span></div>
              <div className="flex justify-between border-b py-1"><span>Revenu déclaré:</span> <span className="font-medium">{formData.income} $</span></div>
              <div className="flex justify-between border-b py-1"><span>Statut:</span> <span className="font-medium">{formData.isCitizen ? 'Citoyen/RP' : 'Non-Citoyen'}</span></div>
              <div className="flex justify-between border-b py-1"><span>Documents:</span> <span className="font-medium">{documents.length} fichier(s)</span></div>
            </div>
          </div>
        )}
        {/* Navigation Footer */}
        <div className="mt-8 flex justify-between items-center">
          <button
            onClick={() => step > 1 ? setStep(step - 1) : navigate('/dashboard')}
            className="px-6 py-2 rounded font-medium text-gray-600 hover:bg-gray-100"
            disabled={step === 1}
          >
            Précédent
          </button>

          <div className="flex gap-3">
            {step > 1 && (
              <button
                onClick={async () => { const res = await saveDraft(); if(res.success) alert('Brouillon sauvegardé !'); else alert('Erreur: ' + res.error); }}
                className="px-6 py-2 rounded font-medium border border-gray-300 text-gray-600 hover:bg-gray-50"
                disabled={isLoading}
              >
                {isLoading ? '...' : 'Sauvegarder le brouillon'}
              </button>
            )}

            {step < 4 ? (
              <button
                onClick={handleNext} // ✅ Utilisation de la nouvelle fonction handleNext
                className="bg-[#003366] text-white px-6 py-2 rounded font-bold hover:bg-blue-900"
                disabled={isLoading}
              >
                {isLoading ? 'Chargement...' : 'Suivant'}
              </button>
            ) : (
              <button
                onClick={async () => { const res = await submitDeclaration(); if(res.success) { alert('Soumis !'); navigate('/dashboard'); } else alert('Erreur: ' + res.error); }}
                className="bg-green-600 text-white px-6 py-2 rounded font-bold hover:bg-green-700"
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