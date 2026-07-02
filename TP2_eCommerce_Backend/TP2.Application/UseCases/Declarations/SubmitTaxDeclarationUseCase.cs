using System;
using System.Linq;
using System.Threading.Tasks;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
using TP2.Application.UseCases.TaxDeclarations;

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
            IProcessAutomaticValidationUseCase validationUseCase)
        {
            _declarationRepo = declarationRepo;
            _userRepo = userRepo;
            _validationUseCase = validationUseCase;
        }
        public async Task<int> ExecuteAsync(int userId, SubmitDeclarationRequestDto request)
        {
            // 1. FILTRE : Validation du montant maximum
            if (request.TotalIncome > 30000)
            {
                throw new ArgumentException("Le montant déclaré ne peut pas dépasser 30 000 $. Veuillez contacter un agent.");
            }

            // 2. Vérifier que l'utilisateur existe
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                throw new InvalidOperationException("Utilisateur non trouvé.");

            // On récupère toutes les déclarations de l'utilisateur pour voir s'il y a déjà un brouillon pour cette année
            var allDeclarations = await _declarationRepo.GetByUserIdAsync(userId);
            var existingDraft = allDeclarations.FirstOrDefault(d => d.TaxYear == request.Year && d.Status == "Draft");

            TaxDeclaration declaration;

            if (existingDraft != null)
            {
                // ✅ CAS 1 : On a trouvé un brouillon, on le met à jour pour éviter d'en créer un nouveau
                declaration = existingDraft;
                declaration.TotalIncome = request.TotalIncome;
                declaration.CitizenshipStatus = request.CitizenshipStatus;
                declaration.Status = "Submitted"; // On passe le statut à "Submitted" (Terminée)
                declaration.SubmissionDate = DateTime.Now; // On enregistre la date de soumission

                await _declarationRepo.UpdateAsync(declaration);
            }
            else
            {
                // ✅ CAS 2 : Aucun brouillon n'existait, on crée une nouvelle déclaration directement soumise
                declaration = new TaxDeclaration
                {
                    UserId = userId,
                    TaxYear = request.Year,
                    TotalIncome = request.TotalIncome,
                    CitizenshipStatus = request.CitizenshipStatus,
                    Status = "Submitted", // On crée directement en statut "Submitted"
                    SubmissionDate = DateTime.Now
                };
                await _declarationRepo.AddAsync(declaration);
            }
            // 5. DÉCLENCHEMENT : Lancer la validation automatique
            await _validationUseCase.ProcessAsync(declaration.Id);

            return declaration.Id;
        }
    }
}