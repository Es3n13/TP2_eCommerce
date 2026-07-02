import { useState, useCallback } from 'react';
import api from '../services/api';
export const useDeclaration = () => {
  const [step, setStep] = useState(1);
  const [isLoading, setIsLoading] = useState(false);
  const [error, setError] = useState(null);
  const [documents, setDocuments] = useState([]);

  const [formData, setFormData] = useState({
    id: null,
    taxYear: new Date().getFullYear(),
    income: 0,
    isCitizen: false,
  });

  const initializeDeclaration = useCallback(async (year) => {
    setIsLoading(true);
    setError(null);
    try {
      const payload = { taxYear: Number(year) };
      const data = await api.post('/TaxDeclarations/initialize', payload);
      const newId = data.declarationId || data.DeclarationId;

      if (newId) {
        setFormData(prev => ({ ...prev, id: newId, taxYear: year }));
        setStep(2);
        return { success: true, id: newId };
      }
      throw new Error("Le serveur n'a pas renvoyé d'ID.");
    } catch (err) {
      const msg = err.response?.data?.error || err.message || "Erreur d'initialisation";
      setError(msg);
      return { success: false, error: msg };
    } finally {
      setIsLoading(false);
    }
  }, []);

  const loadDeclaration = useCallback(async (id) => {
    setIsLoading(true);
    setError(null);
    console.log("🔍 [DEBUG] Tentative de chargement du brouillon ID:", id);
    
    try {
      const data = await api.get('/TaxDeclarations/me');
      console.log("🌐 [DEBUG] Réponse brute du serveur (/TaxDeclarations/me):", data);

      // On vérifie le format de la réponse
      const list = Array.isArray(data) ? data : (data.declarations || data.value || []);
      console.log("📋 [DEBUG] Liste des déclarations extraite:", list);
      
      const decl = list.find(d => String(d.id) === String(id));
      console.log("🎯 [DEBUG] Déclaration trouvée pour l'ID ?", decl ? "OUI" : "NON", decl);

      if (decl) {
        setFormData({
          id: decl.id,
          taxYear: decl.taxYear,
          income: decl.totalIncome || decl.TotalIncome || 0,
          isCitizen: decl.citizenshipStatus === "Citizen",
        });
        console.log("✅ [DEBUG] FormData mis à jour avec succès");
        return { success: true };
      }
      
      console.error("❌ [DEBUG] L'ID existe dans l'URL mais n'est pas présent dans la liste renvoyée par le serveur");
      throw new Error("Brouillon introuvable sur le serveur.");
    } catch (err) {
      console.error("🔥 [DEBUG] Erreur critique lors du chargement:", err);
      const msg = err.response?.data?.error || err.message || "Erreur de chargement";
      setError(msg);
      return { success: false, error: msg };
    } finally {
      setIsLoading(false);
    }
  }, []);

  const saveDraft = useCallback(async () => {
    setIsLoading(true);
    setError(null);
    try {
      if (!formData.id) throw new Error("Aucune déclaration initialisée.");
      const payload = {
        declarationId: formData.id,
        totalIncome: Number(formData.income) || 0,
        citizenshipStatus: formData.isCitizen ? "Citizen" : "Non-Citizen"
      };
      await api.put('/TaxDeclarations/save-draft', payload);
      return { success: true };
    } catch (err) {
      const msg = err.response?.data?.error || err.message || "Erreur de sauvegarde";
      setError(msg);
      return { success: false, error: msg };
    } finally {
      setIsLoading(false);
    }
  }, [formData]);

  const submitDeclaration = useCallback(async () => {
    setIsLoading(true);
    setError(null);
    try {
      const payload = {
        year: Number(formData.taxYear),
        totalIncome: Number(formData.income),
        citizenshipStatus: formData.isCitizen ? "Citizen" : "Non-Citizen",
      };
      await api.post('/TaxDeclarations/submit', payload);
      return { success: true };
    } catch (err) {
      const msg = err.response?.data?.error || err.message || "Erreur de soumission";
      setError(msg);
      return { success: false, error: msg };
    } finally {
      setIsLoading(false);
    }
  }, [formData]);

  const updateField = (field, value) => setFormData(prev => ({ ...prev, [field]: value }));
  const addDocument = (file) => setDocuments(prev => [...prev, file]);

  return {
    step, setStep, formData, updateField,
    documents, addDocument, saveDraft, submitDeclaration,
    initializeDeclaration, loadDeclaration, isLoading, error
  };
};