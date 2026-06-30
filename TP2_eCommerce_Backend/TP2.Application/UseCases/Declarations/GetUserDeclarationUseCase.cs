using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Declarations
{
    public class GetUserDeclarationsUseCase : IGetUserDeclarationsUseCase
    {
        private readonly ITaxDeclarationRepository _declarationRepository;

        public GetUserDeclarationsUseCase(ITaxDeclarationRepository declarationRepository)
        {
            _declarationRepository = declarationRepository;
        }

        public async Task<IEnumerable<TaxDeclarationDto>> ExecuteAsync(int userId)
        {
            // Récupérer les entités depuis le repository
            var declarations = await _declarationRepository.GetByUserIdAsync(userId);
            // Mapper les entités vers des DTOs
            return declarations.Select(d => new TaxDeclarationDto
            {
                Id = d.Id,
                UserId = d.UserId,
                TaxYear = d.TaxYear,
                TotalIncome = d.TotalIncome,
                Status = d.Status,
                SubmissionDate = d.SubmissionDate
            });
        }
    }
}
