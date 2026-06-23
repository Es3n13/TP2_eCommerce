using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Declarations
{
    public class SubmitTaxDeclarationUseCase : ISubmitTaxDeclarationUseCase
    {
        private readonly ITaxDeclarationRepository _declarationRepo;
        private readonly IUserRepository _userRepo;
        public SubmitTaxDeclarationUseCase(
            ITaxDeclarationRepository declarationRepo,
            IUserRepository userRepo)
        {
            _declarationRepo = declarationRepo;
            _userRepo = userRepo;
        }

        public async Task<int> ExecuteAsync(int userId, SubmitDeclarationRequestDto request)
        {
            // 1. Vérifier que l'utilisateur existe (règle métier)
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("Utilisateur non trouvé.");
            // 2. Créer l'entité Domain (le cœur de la règle métier)
            var declaration = new TaxDeclaration
            {
                UserId = userId,
                TaxYear = request.Year,
                TotalIncome = request.TotalIncome,
                CitizenshipStatus = request.CitizenshipStatus,
                Status = "Draft" // Commence toujours en brouillon
            };

            // 3. Sauvegarder via le repository (dépendance du Domain)
            await _declarationRepo.AddAsync(declaration);

            // 4. Retourner l'ID généré
            return declaration.Id;
        }
    }
}
