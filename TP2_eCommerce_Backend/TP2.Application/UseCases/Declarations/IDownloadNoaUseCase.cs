using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;

namespace TP2.Application.UseCases.Declarations
{
    public interface IDownloadNoaUseCase
    {
        Task<FileDownloadDto?> ExecuteAsync(int declarationId);
    }
}
