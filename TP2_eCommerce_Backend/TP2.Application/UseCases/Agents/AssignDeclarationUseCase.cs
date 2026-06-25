using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Agents;

public class AssignDeclarationUseCase
{
    private readonly ITaxDeclarationRepository _repository;

    public AssignDeclarationUseCase(ITaxDeclarationRepository repository)
    {
        _repository = repository;
    }

    public async Task ExecuteAsync(AssignDeclarationRequest request)
    {
        var declaration = await _repository.GetByIdAsync(request.DeclarationId);
        if (declaration == null) throw new Exception("Déclaration non trouvée");

        declaration.AssignedAgentId = request.AgentId;
        await _repository.UpdateAsync(declaration);
    }
}