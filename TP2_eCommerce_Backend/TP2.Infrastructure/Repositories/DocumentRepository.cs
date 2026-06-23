using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
using TP2.Infrastructure.Data;

namespace TP2.Infrastructure.Repositories
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly ApplicationDbContext _context;

        public DocumentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<SupportingDocument>> GetByDeclarationIdAsync(int declarationId) =>
            await _context.SupportingDocuments
                .Where(d => d.DeclarationId == declarationId)
                .ToListAsync();

        public async Task AddAsync(SupportingDocument document)
        {
            await _context.SupportingDocuments.AddAsync(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int docId)
        {
            var doc = await _context.SupportingDocuments.FindAsync(docId);
            if (doc != null)
            {
                _context.SupportingDocuments.Remove(doc);
                await _context.SaveChangesAsync();
            }
        }
    }
}
