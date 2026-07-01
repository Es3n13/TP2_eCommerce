import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useRegister } from '../viewmodels/useRegister';

const RegisterPage = () => {
  const navigate = useNavigate();
  const { register, isLoading, error } = useRegister();

  const [formData, setFormData] = useState({
    email: '',
    password: '',
    firstName: '',
    lastName: '',
    nas: '', // Pour les citoyens
    employeeId: '', // Pour les agents
  });
  const [userType, setUserType] = useState('User'); // 'User' ou 'Agent'

  const handleChange = (e) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await register(userType, formData);
      alert("Compte créé avec succès !");
      navigate('/login');
    } catch (err) {
      console.error("Registration failed:", err);
    }
  };

  return (
    <div className="max-w-md mx-auto mt-10 p-8 bg-white shadow-xl rounded-lg border-t-4 border-[#003366]">
      <h2 className="text-2xl font-bold text-center mb-6 text-[#003366]">Créer un compte</h2>
      
      {/* Sélecteur de type de compte */}
      <div className="flex mb-6 bg-gray-100 p-1 rounded-lg">
        <button 
          onClick={() => setUserType('User')}
          className={`flex-1 py-2 text-sm font-bold rounded-md transition-all ${userType === 'User' ? 'bg-white shadow text-[#003366]' : 'text-gray-500'}`}
        >
          Citoyen
        </button>
        <button 
          onClick={() => setUserType('Agent')}
          className={`flex-1 py-2 text-sm font-bold rounded-md transition-all ${userType === 'Agent' ? 'bg-white shadow text-[#003366]' : 'text-gray-500'}`}
        >
          Agent
        </button>
      </div>

      <form onSubmit={handleSubmit} className="space-y-4">
        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium mb-1">Prénom</label>
            <input name="firstName" onChange={handleChange} className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" required />
          </div>
          <div>
            <label className="block text-sm font-medium mb-1">Nom</label>
            <input name="lastName" onChange={handleChange} className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" required />
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium mb-1">Email</label>
          <input name="email" type="email" onChange={handleChange} className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" required />
        </div>
        {userType === 'User' ? (
          <div>
            <label className="block text-sm font-medium mb-1">Numéro d'Assurance Sociale (NAS)</label>
            <input name="nas" placeholder="000 000 000" onChange={handleChange} className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" required />
          </div>
        ) : (
          <div>
            <label className="block text-sm font-medium mb-1">ID Employé</label>
            <input name="employeeId" onChange={handleChange} className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" required />
          </div>
        )}

        <div>
          <label className="block text-sm font-medium mb-1">Mot de passe</label>
          <input name="password" type="password" onChange={handleChange} className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" required />
        </div>

        {error && <p className="text-red-500 text-xs text-center">{error}</p>}

        <button 
          type="submit"
          disabled={isLoading}
          className="w-full bg-[#003366] text-white py-2 rounded font-bold hover:bg-blue-900 transition-colors disabled:bg-gray-400"
        >
          {isLoading ? 'Création...' : 'S\'inscrire'}
        </button>
      </form>

      <p className="mt-6 text-center text-sm text-gray-500">
        Déjà un compte ? <a href="/login" className="text-[#003366] underline">Se connecter ici</a>
      </p>
    </div>
  );
};

export default RegisterPage;