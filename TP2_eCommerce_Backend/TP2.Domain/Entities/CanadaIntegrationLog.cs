using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
namespace TP2.Domain.Entities
{
    public class CanadaIntegrationLog
    {
        public int Id { get; set; }
        public int DeclarationId { get; set; }
        public DateTime AttemptDate { get; set; }
        public string ErrorMessage { get; set; }
        public int RetryCount { get; set; } // 0 = 1ère tentative, 1 = 1er retry, etc.
        public bool IsSuccessful { get; set; }
    }
}
