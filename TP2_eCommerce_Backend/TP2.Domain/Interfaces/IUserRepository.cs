using System;
using System.Collections.Generic;
using System.Text;
using TP2.Domain.Entities;

namespace TP2.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByNasAsync(string nas);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
