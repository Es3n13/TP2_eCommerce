using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Declarations
{
    public class InitializeDeclarationUseCase : IInitializeDeclarationUseCase
    {
        private readonly ITaxDeclarationRepository _repository;

        public InitializeDeclarationUseCase(ITaxDeclarationRepository repository)
            => _repository = repository;

        public async Task<int> ExecuteAsync(InitializeDeclarationRequestDto request)
        {
            // On vérifie si une déclaration existe déjà pour cette année-là
            var userDeclarations = await _repository.GetByUserIdAsync(request.UserId);
            var existing = userDeclarations.FirstOrDefault(d => d.TaxYear == request.TaxYear);

            if (existing != null) return existing.Id;
            // Sinon, on crée un nouveau brouillon
            var newDeclaration = new TaxDeclaration
            {
                UserId = request.UserId,
                TaxYear = request.TaxYear,
                Status = "Draft",
                TotalIncome = 0
            };

            await _repository.AddAsync(newDeclaration);
            return newDeclaration.Id;
        }
    }
}
