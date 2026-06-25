using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs;

public class DecideDeclarationRequest
{
    public int DeclarationId { get; set; }
    public string AgentNotes { get; set; } = string.Empty;
    public string Decision { get; set; } = string.Empty; // "Validated" ou "Rejected"
}
