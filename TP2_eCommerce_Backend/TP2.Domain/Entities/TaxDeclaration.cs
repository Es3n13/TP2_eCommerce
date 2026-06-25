using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.Domain.Entities
{
    public class TaxDeclaration
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TaxYear { get; set; }
        public decimal TotalIncome { get; set; }
        public string? CitizenshipStatus { get; set; }
        public DateTime? SubmissionDate { get; set; }
        public string Status { get; set; } = "Draft"; // Draft, Submitted, UnderReview, Validated, Rejected
        public string? AgentNotes { get; set; }
        public int? AssignedAgentId { get; set; }

        // Propriétés de navigation (pour faciliter les jointures plus tard)
        public virtual User? User { get; set; }
        public virtual Agent? ReviewingAgent { get; set; }
    }
}
