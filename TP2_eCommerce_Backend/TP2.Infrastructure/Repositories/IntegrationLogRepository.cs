using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
using TP2.Infrastructure.Data;

namespace TP2.Infrastructure.Repositories
{
    public class IntegrationLogRepository : IIntegrationLogRepository
    {
        private readonly ApplicationDbContext _context;

        public IntegrationLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddLogAsync(CanadaIntegrationLog log)
        {
            await _context.CanadaIntegrationLogs.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CanadaIntegrationLog>> GetLogsByDeclarationIdAsync(int declarationId) =>
            await _context.CanadaIntegrationLogs
                .Where(l => l.DeclarationId == declarationId)
                .ToListAsync();
    }
}