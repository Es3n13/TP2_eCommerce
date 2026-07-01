import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../viewmodels/useAuth';
const LoginPage = () => {
  const [email, setEmail] = useState('');
  const [password, setPassword] = useState('');
  
  const { login, isLoading, error } = useAuth();
  const navigate = useNavigate();

  const handleLogin = async (e) => {
    e.preventDefault();
    try {
      const authData = await login(email, password);
      if (authData.role === 'Agent') {
        navigate('/agent-queue');
      } else {
        navigate('/dashboard');
      }
    } catch (err) {
      console.error("Login failed:", err);
    }
  };

  return (
    <div className="max-w-md mx-auto mt-20 p-8 bg-white shadow-xl rounded-lg border-t-4 border-[#003366]">
      <h2 className="text-2xl font-bold text-center mb-6 text-[#003366]">Accès Sécurisé</h2>
      
      <form onSubmit={handleLogin} className="space-y-4">
        <div>
          <label className="block text-sm font-medium mb-1">Email / Identifiant</label>
          <input 
            type="email" 
            placeholder="exemple@mail.com" 
            className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" 
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />
        </div>
        <div>
          <label className="block text-sm font-medium mb-1">Mot de passe</label>
          <input 
            type="password" 
            className="w-full p-2 border rounded focus:ring-2 focus:ring-[#003366] outline-none" 
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
        </div>
        
        {error && <p className="text-red-500 text-xs text-center">{error}</p>}
        
        <button 
          type="submit"
          disabled={isLoading}
          className="w-full bg-[#003366] text-white py-2 rounded font-bold hover:bg-blue-900 transition-colors disabled:bg-gray-400"
        >
          {isLoading ? 'Connexion...' : 'Se connecter'}
        </button>
      </form>
      <p className="mt-6 text-center text-sm text-gray-500">
        Nouveau compte ? <a href="/register" className="text-[#003366] underline">S'inscrire ici</a>
      </p>
    </div>
  );
};

export default LoginPage;
