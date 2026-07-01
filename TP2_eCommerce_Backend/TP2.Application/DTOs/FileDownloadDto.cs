using System;
using System.Collections.Generic;
using System.Text;

namespace TP2.Application.DTOs
{
    public class FileDownloadDto
    {
        public byte[] Content { get; set; } = Array.Empty<byte>();
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = "application/pdf";
    }
}
