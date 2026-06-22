import React from 'react';
import { Link, Outlet } from 'react-router-dom';

const Layout = ({ children, role = 'user' }) => {
  return (
    <div className="min-h-screen flex flex-col">
      <header className="bg-[#003366] text-white p-4 shadow-md flex justify-between items-center">
        <div className="flex items-center gap-2">
          <span className="text-2xl">⚜️</span>
          <h1 className="text-xl font-bold tracking-tight">Revenu Québec - Portail Citoyen</h1>
        </div>
        <nav className="flex gap-4 items-center">
          {role === 'user' ? (
            <>
              <Link to="/dashboard" className="hover:underline">Mon Dossier</Link>
              <Link to="/login" className="bg-white text-[#003366] px-3 py-1 rounded font-medium">Déconnexion</Link>
            </>
          ) : (
            <>
              <Link to="/agent/queue" className="hover:underline">File d'attente</Link>
              <Link to="/login" className="bg-white text-[#003366] px-3 py-1 rounded font-medium">Quitter</Link>
            </>
          )}
        </nav>
      </header>
      
      <main className="flex-1 container mx-auto p-6">
        <Outlet />
      </main>

      <footer className="bg-blue-200 text-white-600 p-4 text-center text-sm border-t">
        © 2026 Gouvernement du Québec - Système de déclaration TP2
      </footer>
    </div>
  );
};

export default Layout;
