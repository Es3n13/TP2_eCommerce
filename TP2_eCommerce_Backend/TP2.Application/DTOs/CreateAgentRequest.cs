using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace TP2.Application.DTOs;

public class CreateAgentRequest
{
    [Required]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    public string FullName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty; // En prod, on  devrait hacher le mot de passe
}
