using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs
{
    public class SubmitDeclarationRequestDto
    {
        public int UserId { get; set; }
        public int Year { get; set; }
        public decimal TotalIncome { get; set; }
        public string? CitizenshipStatus { get; set; }
        // On supposera que l'ID de l'utilisateur vient du token JWT (à voir plus tard)
    }
}
