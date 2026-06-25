using System;
using System.Threading.Tasks;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
using TP2.Application.UseCases.TaxDeclarations; // Assure-toi que le namespace est correct

namespace TP2.Application.UseCases.Declarations
{
    public class SubmitTaxDeclarationUseCase : ISubmitTaxDeclarationUseCase
    {
        private readonly ITaxDeclarationRepository _declarationRepo;
        private readonly IUserRepository _userRepo;
        private readonly IProcessAutomaticValidationUseCase _validationUseCase;

        public SubmitTaxDeclarationUseCase(
            ITaxDeclarationRepository declarationRepo,
            IUserRepository userRepo,
            IProcessAutomaticValidationUseCase validationUseCase) // Injection du Use Case de validation
        {
            _declarationRepo = declarationRepo;
            _userRepo = userRepo;
            _validationUseCase = validationUseCase;
        }

        public async Task<int> ExecuteAsync(int userId, SubmitDeclarationRequestDto request)
        {
            // 1. FILTRE : Validation du montant maximum (Règle métier stricte)
            if (request.TotalIncome > 30000)
            {
                throw new ArgumentException("Le montant déclaré ne peut pas dépasser 30 000 $. Veuillez contacter un agent.");
            }

            // 2. Vérifier que l'utilisateur existe
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("Utilisateur non trouvé.");

            // 3. Créer l'entité Domain
            var declaration = new TaxDeclaration
            {
                UserId = userId,
                TaxYear = request.Year,
                TotalIncome = request.TotalIncome,
                CitizenshipStatus = request.CitizenshipStatus,
                Status = "Draft"
            };

            // 4. Sauvegarder
            await _declarationRepo.AddAsync(declaration);
            // 5. DÉCLENCHEMENT : Lancer la validation automatique immédiatement après la sauvegarde
            await _validationUseCase.ProcessAsync(declaration.Id);

            return declaration.Id;
        }
    }
}