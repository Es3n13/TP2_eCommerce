using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
using TP2.Infrastructure.Data;
namespace TP2.Infrastructure.Repositories
{
    public class TaxDeclarationRepository : ITaxDeclarationRepository
    {
        private readonly ApplicationDbContext _context;

        public TaxDeclarationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<TaxDeclaration?> GetByIdAsync(int id) =>
            await _context.TaxDeclarations.FindAsync(id);

        public async Task<IEnumerable<TaxDeclaration>> GetByUserIdAsync(int userId) =>
            await _context.TaxDeclarations.Where(d => d.UserId == userId).ToListAsync();

        public async Task<IEnumerable<TaxDeclaration>> GetPendingReviewAsync() =>
            await _context.TaxDeclarations.Where(d => d.Status == "Submitted").ToListAsync();

        public async Task<List<TaxDeclaration>> GetAllAsync()
        {
            return await _context.TaxDeclarations.ToListAsync();
        }

        public async Task AddAsync(TaxDeclaration declaration)
        {
            await _context.TaxDeclarations.AddAsync(declaration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TaxDeclaration declaration)
        {
            _context.TaxDeclarations.Update(declaration);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateStatusAsync(int id, string status)
        {
            var dec = await _context.TaxDeclarations.FindAsync(id);
            if (dec != null)
            {
                dec.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
