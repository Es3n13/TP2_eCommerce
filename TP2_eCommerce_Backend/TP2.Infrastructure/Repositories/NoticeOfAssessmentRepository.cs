using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
using TP2.Infrastructure.Data;

namespace TP2.Infrastructure.Repositories
{
    public class NoticeOfAssessmentRepository : INoticeOfAssessmentRepository
    {
        private readonly ApplicationDbContext _context;

        public NoticeOfAssessmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<NoticeOfAssessment?> GetByIdAsync(int id) =>
            await _context.NoticesOfAssessment.FindAsync(id);

        public async Task<NoticeOfAssessment?> GetByDeclarationIdAsync(int declarationId) =>
            await _context.NoticesOfAssessment
                .FirstOrDefaultAsync(n => n.DeclarationId == declarationId);
        public async Task AddAsync(NoticeOfAssessment notice)
        {
            await _context.NoticesOfAssessment.AddAsync(notice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(NoticeOfAssessment notice)
        {
            _context.NoticesOfAssessment.Update(notice);
            await _context.SaveChangesAsync();
        }
    }
}
