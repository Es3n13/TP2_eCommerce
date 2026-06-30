using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;

namespace TP2.Application.UseCases.Declarations
{
    public interface IInitializeDeclarationUseCase
    {
        Task<int> ExecuteAsync(InitializeDeclarationRequestDto request);
    }

    public interface ISaveDeclarationDraftUseCase
    {
        Task<bool> ExecuteAsync(SaveDeclarationDraftRequestDto request);
    }

    public interface IUploadSupportingDocumentUseCase
    {
        Task<int> ExecuteAsync(int declarationId, string fileName, byte[] fileContent);
    }
}
