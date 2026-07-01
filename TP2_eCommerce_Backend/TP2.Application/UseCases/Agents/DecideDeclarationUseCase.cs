using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;
namespace TP2.Application.UseCases.Agents;

public class DecideDeclarationUseCase
{
    private readonly ITaxDeclarationRepository _repository;
    private readonly INoticeOfAssessmentRepository _noaRepository;

    public DecideDeclarationUseCase(
        ITaxDeclarationRepository repository,
        INoticeOfAssessmentRepository noaRepository)
    {
        _repository = repository;
        _noaRepository = noaRepository;
    }

    public async Task ExecuteAsync(DecideDeclarationRequest request)
    {
        var declaration = await _repository.GetByIdAsync(request.DeclarationId);
        if (declaration == null) throw new Exception("Déclaration non trouvée");

        declaration.AgentNotes = request.AgentNotes;
        declaration.Status = request.Decision; // "Validated" ou "Rejected"

        await _repository.UpdateAsync(declaration);

        // On transforme la décision de l'agent en un document officiel en base de données
        var notice = new NoticeOfAssessment
        {
            DeclarationId = request.DeclarationId,
            DecisionResult = request.Decision,
            Notes = request.AgentNotes,
            IssueDate = DateTime.UtcNow,
            PdfPath = $"/uploads/noa/NOA_{request.DeclarationId}_{DateTime.UtcNow:yyyyMMdd}.pdf"
        };

        await _noaRepository.AddAsync(notice);
    }
}
