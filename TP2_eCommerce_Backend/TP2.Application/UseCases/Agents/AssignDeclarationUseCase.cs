using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Agents;

public class AssignDeclarationUseCase
{
    private readonly ITaxDeclarationRepository _repository;
    private readonly IAgentRepository _agentRepository;

    public AssignDeclarationUseCase(ITaxDeclarationRepository repository,
        IAgentRepository agentRepository)
    {
        _repository = repository;
        _agentRepository = agentRepository;
    }

    public async Task ExecuteAsync(AssignDeclarationRequest request)
    {
        // Vérifier que la déclaration existe
        var declaration = await _repository.GetByIdAsync(request.DeclarationId);
        if (declaration == null) throw new Exception("Déclaration non trouvée");

        // On cherche l'Agent par son EmployeeId (string)
        var agent = await _agentRepository.GetByEmployeeIdAsync(request.EmployeeId);

        if (agent == null)
            throw new Exception($"L'agent avec l'ID employé '{request.EmployeeId}' n'existe pas.");
        // On assigne l'ID technique (int) trouvé dans l'objet Agent
        declaration.AssignedAgentId = agent.Id;

        await _repository.UpdateAsync(declaration);
    }
}