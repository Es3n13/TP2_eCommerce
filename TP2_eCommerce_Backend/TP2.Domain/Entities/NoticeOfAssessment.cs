using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.Domain.Entities
{
    public class NoticeOfAssessment
    {
        public int Id { get; set; }
        public int DeclarationId { get; set; }
        public decimal FinalAmount { get; set; }
        public DateTime IssueDate { get; set; } = DateTime.UtcNow;
        public bool IsAutomated { get; set; } = true;
        public string PdfPath { get; set; } = string.Empty;

        public virtual TaxDeclaration? TaxDeclaration { get; set; }
    }
}
