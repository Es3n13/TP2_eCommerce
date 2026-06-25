using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Agents
{
    public class GetPendingReviewsUseCase
    {
        private readonly ITaxDeclarationRepository _declarationRepo;

        public GetPendingReviewsUseCase(ITaxDeclarationRepository declarationRepo)
        {
            _declarationRepo = declarationRepo;
        }

        public async Task<List<PendingReviewDto>> ExecuteAsync()
        {
            // 1. Récupérer toutes les déclarations (ou créer une méthode spécifique dans le repo)
            var allDeclarations = await _declarationRepo.GetAllAsync();
            // 2. Filtrer uniquement celles qui sont "UnderReview" et les mapper vers le DTO
            return allDeclarations
                .Where(d => d.Status == "UnderReview")
                .Select(d => new PendingReviewDto
                {
                    Id = d.Id,
                    UserId = d.UserId,
                    TotalIncome = d.TotalIncome,
                    TaxYear = d.TaxYear,
                    AgentNotes = d.AgentNotes,
                    Status = d.Status
                })
                .ToList();
        }
    }
}
