import { useState } from 'react';
import api from '../services/api';

export const useDeclaration = () => {
  const [step, setStep] = useState(1); 
  // État des données du formulaire (Format interne pour l'UI)
  const [formData, setFormData] = useState({
    id: null,           
    firstName: '',
    lastName: '',
    address: '',
    income: 0,          
    isCitizen: false,
  });
  
  const [documents, setDocuments] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  // Action pour initialiser une déclaration
  const initializeDeclaration = async (declarationId = null) => {
    setIsLoading(true);
    try {
        const endpoint = declarationId 
        ? `/TaxDeclarations/initialize?id=${declarationId}` 
        : '/TaxDeclarations/initialize';
        
        const data = await api.get(endpoint);
        console.log("📩 Réponse reçue de /initialize :", data);
        
        if (data) {
            setFormData({
                ...data.userData, 
                id: data.id 
            });
        }
        return { success: true, data };
    } catch (err) {
        setError(err.message);
        return { success: false, error: err.message };
    } finally {
        setIsLoading(false);
    }
  };

  const updateField = (field, value) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };
  const addDocument = (file) => {
    setDocuments(prev => [...prev, file]);
  };

  // 🦀 ACTION CORRIGÉE : Mapping des données pour l'API
  const saveDraft = async () => {
    setIsLoading(true);
    setError(null);

    try {
        // 🎯 TRADUCTION : On convertit le format UI vers le format API
        const payload = {
            declarationId: formData.id,                      // id -> declarationId
            totalIncome: Number(formData.income),            // income -> totalIncome (on force le nombre)
            citizenshipStatus: formData.isCitizen ? "Citizen" : "Non-Citizen" // isCitizen -> citizenshipStatus
        };

        console.log("🚀 Payload envoyé au serveur :", payload);
        
        await api.put('/TaxDeclarations/save-draft', payload);
        return { success: true };
    } catch (err) {
        const errorMessage = err.message || "Erreur lors de la sauvegarde du brouillon";
        setError(errorMessage);
        return { success: false, error: errorMessage };
    } finally {
        setIsLoading(false);
    }
  };
  const submitDeclaration = async () => {
    setIsLoading(true);
    setError(null);
    try {
      const data = new FormData();
      
      Object.entries(formData).forEach(([key, value]) => {
        data.append(key, value);
      });
      documents.forEach(doc => data.append('files', doc));

      await api.post('/TaxDeclarations/submit', data);
      return { success: true };
    } catch (err) {
      const errorMessage = err.message || "Erreur lors de la soumission";
      setError(errorMessage);
      return { success: false, error: errorMessage };
    } finally {
        setIsLoading(false);
    }
  };

  return {
    step,
    setStep,
    formData,
    setFormData,
    updateField,
    documents,
    addDocument,
    saveDraft,
    submitDeclaration,
    isLoading,
    error
  };
};