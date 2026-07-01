using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TP2.Domain.Entities
{
    public class NoticeOfAssessment
    {
        public int Id { get; set; }
        public int DeclarationId { get; set; }
        public string DecisionResult { get; set; } = string.Empty; // "Validated" ou "Rejected"
        public string Notes { get; set; } = string.Empty;
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public string PdfPath { get; set; } = string.Empty;

        public virtual TaxDeclaration? TaxDeclaration { get; set; }
    }
}
