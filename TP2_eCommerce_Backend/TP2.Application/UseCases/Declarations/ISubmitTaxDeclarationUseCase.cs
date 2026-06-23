using System;
using System.Collections.Generic;
using System.Text;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Declarations
{
    public interface ISubmitTaxDeclarationUseCase
    {
        Task<int> ExecuteAsync(int userId, SubmitDeclarationRequestDto request);
    }
}
