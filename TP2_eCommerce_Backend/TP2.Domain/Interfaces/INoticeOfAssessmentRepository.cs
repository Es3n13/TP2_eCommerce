using System;
using System.Collections.Generic;
using System.Text;
using TP2.Domain.Entities;

namespace TP2.Domain.Interfaces
{
    public interface INoticeOfAssessmentRepository
    {
        Task<NoticeOfAssessment?> GetByIdAsync(int id);
        Task<NoticeOfAssessment?> GetByDeclarationIdAsync(int declarationId);
        Task AddAsync(NoticeOfAssessment notice);
        Task UpdateAsync(NoticeOfAssessment notice);
    }
}
