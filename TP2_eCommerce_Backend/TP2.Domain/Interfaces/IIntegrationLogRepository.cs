using System;
using System.Collections.Generic;
using System.Text;
using TP2.Domain.Entities;

namespace TP2.Domain.Interfaces
{
    public interface IIntegrationLogRepository
    {
        Task AddLogAsync(CanadaIntegrationLog log);
        Task<IEnumerable<CanadaIntegrationLog>> GetLogsByDeclarationIdAsync(int declarationId);
    }
}
