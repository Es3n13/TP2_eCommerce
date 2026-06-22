import React from 'react';
import { useNavigate } from 'react-router-dom';

const LoginPage = () => {
  const navigate = useNavigate();

  return (
    <div className="max-w-md mx-auto mt-20 p-8 bg-white shadow-xl rounded-lg border-t-4 border-[#003366]">
      <h2 className="text-2xl font-bold text-center mb-6 text-[#003366]">Accès Sécurisé</h2>
      <div className="space-y-4">
        <div>
          <label className="block text-sm font-medium mb-1">Numéro d'Assurance Sociale (NAS)</label>
          <input type="text" placeholder="000 000 000" className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" />
        </div>
        <div>
          <label className="block text-sm font-medium mb-1">Mot de passe</label>
          <input type="password" className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" />
        </div>
        <button 
          onClick={() => navigate('/dashboard')}
          className="w-full bg-[#003366] text-white py-2 rounded font-bold hover:bg-blue-900 transition-colors"
        >
          Se connecter
        </button>
      </div>
      <p className="mt-6 text-center text-sm text-gray-500">
        Nouveau compte ? <a href="#" className="text-[#003366] underline">S'inscrire ici</a>
      </p>
    </div>
  );
};

export default LoginPage;
