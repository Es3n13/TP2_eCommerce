using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TP2.Application.UseCases.TaxDeclarations
{
    public interface IProcessAutomaticValidationUseCase
    {
        Task ProcessAsync(int declarationId);
    }
}
