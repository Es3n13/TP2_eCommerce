import React from 'react';
import { useNavigate } from 'react-router-dom';

const AgentQueuePage = () => {
  const navigate = useNavigate();

  const queue = [
    { id: '45821', user: 'Jean Tremblay', date: '2026-06-20', amount: '24 500 $', status: 'UnderReview' },
    { id: '45822', user: 'Marie Côté', date: '2026-06-21', amount: '12 000 $', status: 'UnderReview' },
    { id: '45823', user: 'Luc Picard', date: '2026-06-21', amount: '29 900 $', status: 'UnderReview' },
  ];

  return (
    <div className="space-y-6">
      <h2 className="text-3xl font-bold text-[#003366]">File d'attente des Revues</h2>
      
      <div className="bg-white shadow-sm border rounded-xl overflow-hidden">
        <table className="w-full text-left border-collapse">
          <thead className="bg-gray-50 border-b">
            <tr>
              <th className="p-4 font-bold text-gray-600">ID Dossier</th>
              <th className="p-4 font-bold text-gray-600">Contribuable</th>
              <th className="p-4 font-bold text-gray-600">Date</th>
              <th className="p-4 font-bold text-gray-600">Montant</th>
              <th className="p-4 font-bold text-gray-600">Action</th>
            </tr>
          </thead>
          <tbody>
            {queue.map(item => (
              <tr key={item.id} className="border-b hover:bg-gray-50 transition-colors">
                <td className="p-4 font-mono text-sm">{item.id}</td>
                <td className="p-4 font-medium">{item.user}</td>
                <td className="p-4 text-gray-500">{item.date}</td>
                <td className="p-4 font-bold">{item.amount}</td>
                <td className="p-4">
                  <button 
                    onClick={() => navigate(`/agent/review/${item.id}`)}
                    className="bg-[#003366] text-white px-3 py-1 rounded text-sm font-bold hover:bg-blue-900"
                  >
                    Ouvrir
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
};

export default AgentQueuePage;
