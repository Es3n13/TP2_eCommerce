using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Agents;

public class CreateAgentUseCase
{
    private readonly IAgentRepository _agentRepository;

    public CreateAgentUseCase(IAgentRepository agentRepository)
    {
        _agentRepository = agentRepository;
    }

    public async Task<int> ExecuteAsync(CreateAgentRequest request)
    {
        // Vérification optionnelle : l'EmployeeId est-il déjà utilisé ?
        var existingAgent = await _agentRepository.GetByEmployeeIdAsync(request.EmployeeId);
        if (existingAgent != null)
            throw new InvalidOperationException($"Un agent avec l'ID employé '{request.EmployeeId}' existe déjà.");

        // Mapping : DTO → Entité
        var agent = new Agent
        {
            EmployeeId = request.EmployeeId,
            FullName = request.FullName,
            Email = request.Email,
            PasswordHash = request.Password, // ⚠️ En production : hacher le mot de passe (ex: BCrypt)
            CreatedAt = DateTime.UtcNow
        };

        // Persistance
        await _agentRepository.AddAsync(agent);
        return agent.Id; // Retourner l'ID généré
    }
}
