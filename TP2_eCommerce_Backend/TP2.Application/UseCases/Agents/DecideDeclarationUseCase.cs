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

    public DecideDeclarationUseCase(ITaxDeclarationRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(DecideDeclarationRequest request)
    {
        var declaration = await _repository.GetByIdAsync(request.DeclarationId);
        if (declaration == null) throw new Exception("Déclaration non trouvée");

        // Mise à jour des notes et du statut
        declaration.AgentNotes = request.AgentNotes;
        declaration.Status = request.Decision; // "Validated" ou "Rejected"

        await _repository.UpdateAsync(declaration);
    }
}
