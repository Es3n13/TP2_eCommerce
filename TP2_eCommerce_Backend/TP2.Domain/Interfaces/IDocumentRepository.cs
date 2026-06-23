using System;
using System.Collections.Generic;
using System.Text;
using TP2.Domain.Entities;

namespace TP2.Domain.Interfaces
{
    public interface IDocumentRepository
    {
        Task<IEnumerable<SupportingDocument>> GetByDeclarationIdAsync(int declarationId);
        Task AddAsync(SupportingDocument document);
        Task DeleteAsync(int docId);
    }
}
