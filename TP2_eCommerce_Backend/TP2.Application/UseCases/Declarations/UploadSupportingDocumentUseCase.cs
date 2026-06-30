using System;
using System.Collections.Generic;
using System.Text;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Declarations
{
    public class UploadSupportingDocumentUseCase : IUploadSupportingDocumentUseCase
    {
        private readonly ITaxDeclarationRepository _taxRepo;
        private readonly IDocumentRepository _docRepo;
        private readonly string _uploadFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Uploads");

        public UploadSupportingDocumentUseCase(ITaxDeclarationRepository taxRepo, IDocumentRepository docRepo)
        {
            _taxRepo = taxRepo;
            _docRepo = docRepo;
            if (!Directory.Exists(_uploadFolder)) Directory.CreateDirectory(_uploadFolder);
        }

        public async Task<int> ExecuteAsync(int declarationId, string fileName, byte[] fileContent)
        {
            var declaration = await _taxRepo.GetByIdAsync(declarationId);
            if (declaration == null) throw new Exception("Déclaration non trouvée.");

            // On rend le nom unique pour éviter les collisions de fichiers
            string uniqueFileName = $"{Guid.NewGuid()}_{fileName}";
            string filePath = Path.Combine(_uploadFolder, uniqueFileName);

            await File.WriteAllBytesAsync(filePath, fileContent);

            var document = new SupportingDocument
            {
                DeclarationId = declarationId,
                FileName = fileName,
                FilePath = filePath,
                UploadDate = DateTime.UtcNow
            };
            await _docRepo.AddAsync(document);
            return document.Id;
        }
    }
}
