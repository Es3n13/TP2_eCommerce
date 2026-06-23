using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TP2.Application.DTOs;

namespace TP2.Application.UseCases.Users
{
    public interface ICreateUserUseCase
    {
        Task<int> ExecuteAsync(CreateUserRequestDto request);
    }
}
