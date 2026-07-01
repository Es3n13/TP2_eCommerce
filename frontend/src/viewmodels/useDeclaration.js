import { useState } from 'react';
import api from '../services/api';

export const useDeclaration = () => {
  // État des données du formulaire
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    address: '',
    income: '',
    isCitizen: false,
  });

  const [documents, setDocuments] = useState([]);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);

  // Action pour mettre à jour un champ
  const updateField = (field, value) => {
    setFormData(prev => ({ ...prev, [field]: value }));
  };

  // Action pour ajouter un document
  const addDocument = (file) => {
    setDocuments(prev => [...prev, file]);
  };

  // Action de soumission finale
  const submitDeclaration = async () => {
    setIsLoading(true);
    setError(null);
    try {
      const data = new FormData();
      data.append('userData', JSON.stringify(formData));
      documents.forEach(doc => data.append('files', doc));

      await api.post('/declaration/submit', data);
      return { success: true };
    } catch (err) {
      setError(err.response?.data?.message || "Erreur lors de la soumission");
      return { success: false, error };
    } finally {
      setIsLoading(false);
    }
  };

  return {
    formData,
    updateField,
    documents,
    addDocument,
    submitDeclaration,
    isLoading,
    error
  };
};