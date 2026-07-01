using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Declarations
{
    public class DownloadNoaUseCase : IDownloadNoaUseCase
    {
        private readonly INoticeOfAssessmentRepository _noaRepo;

        public DownloadNoaUseCase(INoticeOfAssessmentRepository noaRepo)
        {
            _noaRepo = noaRepo;
        }

        public async Task<FileDownloadDto?> ExecuteAsync(int declarationId)
        {
            // Chercher l'avis de cotisation en base
            var noa = await _noaRepo.GetByDeclarationIdAsync(declarationId);
            if (noa == null) return null;

            // Vérifier si le fichier existe physiquement sur le serveur
            if (!System.IO.File.Exists(noa.PdfPath))
            {
                throw new FileNotFoundException("Le fichier PDF de l'avis de cotisation est introuvable sur le serveur.");
            }

            // Lire les octets du fichier
            var bytes = await System.IO.File.ReadAllBytesAsync(noa.PdfPath);

            return new FileDownloadDto
            {
                Content = bytes,
                FileName = $"Avis_Cotisation_{declarationId}.pdf",
                ContentType = "application/pdf"
            };
        }
    }
}
