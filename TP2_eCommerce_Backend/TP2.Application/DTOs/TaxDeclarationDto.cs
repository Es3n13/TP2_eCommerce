using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs
{
    public class TaxDeclarationDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaxYear { get; set; }
        public decimal TotalIncome { get; set; }
        public string? CitizenshipStatus { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string Status { get; set; } = string.Empty;
        public string? AgentNotes { get; set; }
    }
}
