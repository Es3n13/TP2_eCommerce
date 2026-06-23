using System;
using System.Collections.Generic;
using System.Text;
using TP2.Domain.Entities;

namespace TP2.Domain.Interfaces
{
    public interface ITaxDeclarationRepository
    {
        Task<TaxDeclaration?> GetByIdAsync(int id);
        Task<IEnumerable<TaxDeclaration>> GetByUserIdAsync(int userId);
        Task<IEnumerable<TaxDeclaration>> GetPendingReviewAsync(); // Pour l'agent
        Task AddAsync(TaxDeclaration declaration);
        Task UpdateAsync(TaxDeclaration declaration);
        Task UpdateStatusAsync(int id, string status);
    }
}
