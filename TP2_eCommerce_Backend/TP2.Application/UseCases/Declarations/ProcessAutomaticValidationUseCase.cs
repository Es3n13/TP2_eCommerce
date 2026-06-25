using System;
using System.Linq;
using System.Threading.Tasks;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.TaxDeclarations
{
    public class ProcessAutomaticValidationUseCase : IProcessAutomaticValidationUseCase
    {
        private readonly ITaxDeclarationRepository _declarationRepo;
        private readonly ICanadaRevenueService _canadaService;
        private readonly IIntegrationLogRepository _logRepo;

        public ProcessAutomaticValidationUseCase(
            ITaxDeclarationRepository declarationRepo,
            ICanadaRevenueService canadaService,
            IIntegrationLogRepository logRepo)
        {
            _declarationRepo = declarationRepo;
            _canadaService = canadaService;
            _logRepo = logRepo;
        }

        public async Task ProcessAsync(int declarationId)
        {
            var declaration = await _declarationRepo.GetByIdAsync(declarationId);
            if (declaration == null) throw new Exception("Déclaration non trouvée");

            int attempts = 0;
            bool success = false;

            while (attempts < 3 && !success)
            {
                attempts++;
                try
                {
                    decimal officialIncome = await _canadaService.GetOfficialIncomeAsync(declaration.UserId);

                    // --- SUCCÈS ---
                    await _logRepo.AddLogAsync(new CanadaIntegrationLog
                    {
                        DeclarationId = declarationId,
                        AttemptDate = DateTime.UtcNow,
                        IsSuccessful = true,
                        ErrorMessage = "Succès"
                    });

                    if (declaration.TotalIncome == officialIncome)
                    {
                        declaration.Status = "Validated";
                        declaration.SubmissionDate = DateTime.UtcNow;
                    }
                    else
                    {
                        declaration.Status = "UnderReview";
                        declaration.AgentNotes = $"Anomalie : Montant différent...";
                    }

                    await _declarationRepo.UpdateAsync(declaration);
                    success = true; // On sort de la boucle
                }
                catch (Exception ex)
                {
                    // --- ÉCHEC ---
                    await _logRepo.AddLogAsync(new CanadaIntegrationLog
                    {
                        DeclarationId = declarationId,
                        AttemptDate = DateTime.UtcNow,
                        IsSuccessful = false,
                        ErrorMessage = ex.Message,
                        RetryCount = attempts
                    });

                    if (attempts >= 3) // Échec final après 3 tentatives
                    {
                        declaration.Status = "UnderReview";
                        declaration.AgentNotes = $"Échec technique : Revenu Canada indisponible après 3 tentatives. Erreur : {ex.Message}";
                        await _declarationRepo.UpdateAsync(declaration);
                    }
                    else
                    {
                        // Petite pause avant de retenter
                        await Task.Delay(500);
                    }
                }
            }
        }
    }
}