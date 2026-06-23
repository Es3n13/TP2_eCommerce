using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.Domain.Entities
{
    public class CanadaIntegrationLog
    {
        public int Id { get; set; }
        public int DeclarationId { get; set; }
        public int AttemptNumber { get; set; }
        public DateTime AttemptTime { get; set; } = DateTime.UtcNow;
        public string Status { get; set; } = string.Empty; // Success, Failed, Timeout
        public string? ErrorMessage { get; set; }
    }
}
