using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs
{
    public class PendingReviewDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalIncome { get; set; }
        public int TaxYear { get; set; }
        public string AgentNotes { get; set; }
        public string Status { get; set; } = "UnderReview";
    }
}
