using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs;

public class AssignDeclarationRequest
{
    public int DeclarationId { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
}
