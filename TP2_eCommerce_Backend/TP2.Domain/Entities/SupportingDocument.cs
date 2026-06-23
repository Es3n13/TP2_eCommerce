using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TP2.Domain.Entities
{
    public class SupportingDocument
    {
        public int Id { get; set; }
        public int DeclarationId { get; set; }
        public string FileName { get; set; } = string.Empty;
        public string FilePath { get; set; } = string.Empty;
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public virtual TaxDeclaration? TaxDeclaration { get; set; }
    }
}
