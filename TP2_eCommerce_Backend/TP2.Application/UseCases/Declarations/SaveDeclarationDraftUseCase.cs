using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Declarations
{
    public class SaveDeclarationDraftUseCase : ISaveDeclarationDraftUseCase
    {
        private readonly ITaxDeclarationRepository _repository;

        public SaveDeclarationDraftUseCase(ITaxDeclarationRepository repository)
            => _repository = repository;

        public async Task<bool> ExecuteAsync(SaveDeclarationDraftRequestDto request)
        {
            var declaration = await _repository.GetByIdAsync(request.DeclarationId);

            if (declaration == null) throw new Exception("Déclaration non trouvée.");

            // On ne peut modifier que si c'est encore un brouillon
            if (declaration.Status != "Draft")
                throw new Exception("Seules les déclarations en mode 'Brouillon' peuvent être modifiées.");

            declaration.TotalIncome = request.TotalIncome;
            declaration.CitizenshipStatus = request.CitizenshipStatus;

            await _repository.UpdateAsync(declaration);
            return true;
        }
    }
}
