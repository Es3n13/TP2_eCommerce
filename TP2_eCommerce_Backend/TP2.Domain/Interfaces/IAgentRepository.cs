using System;
using System.Collections.Generic;
using System.Text;
using TP2.Domain.Entities;

namespace TP2.Domain.Interfaces
{
    public interface IAgentRepository
    {
        Task<Agent?> GetByIdAsync(int id);
        Task<Agent?> GetByEmployeeIdAsync(string employeeId);
        Task<Agent?> GetByEmailAsync(string email);
        Task AddAsync(Agent agent);
        Task UpdateAsync(Agent agent);
    }
}
