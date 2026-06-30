using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs
{
    // Pour l'initialisation
    public class InitializeDeclarationRequestDto
    {
        public int UserId { get; set; }
        public int TaxYear { get; set; }
    }

    // Pour la sauvegarde du brouillon
    public class SaveDeclarationDraftRequestDto
    {
        public int DeclarationId { get; set; }
        public decimal TotalIncome { get; set; }
        public string? CitizenshipStatus { get; set; }
    }
}
