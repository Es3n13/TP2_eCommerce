using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
using TP2.Infrastructure.Data;

namespace TP2.Infrastructure.Repositories
{
    public class AgentRepository : IAgentRepository
    {
        private readonly ApplicationDbContext _context;

        public AgentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Agent?> GetByIdAsync(int id) =>
            await _context.Agents.FindAsync(id);

        public async Task<Agent?> GetByEmployeeIdAsync(string employeeId) =>
            await _context.Agents.FirstOrDefaultAsync(a => a.EmployeeId == employeeId);

        public async Task AddAsync (Agent agent)
        {
            await _context.Agents.AddAsync(agent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Agent agent)
        {
            _context.Agents.Update(agent);
            await _context.SaveChangesAsync();
        }
    }
}
