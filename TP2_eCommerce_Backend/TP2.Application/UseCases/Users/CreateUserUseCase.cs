using System;
using System.Threading.Tasks;
using TP2.Application.DTOs;
using TP2.Domain.Entities;
using TP2.Domain.Interfaces;

namespace TP2.Application.UseCases.Users
{
    public class CreateUserUseCase : ICreateUserUseCase
    {
        private readonly IUserRepository _userRepo;
        public CreateUserUseCase(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task<int> ExecuteAsync(CreateUserRequestDto request)
        {
            // 1. Vérification : on ne veut pas deux utilisateurs avec le même NAS
            var existingUser = await _userRepo.GetByNasAsync(request.Nas);
            if (existingUser != null)
                throw new InvalidOperationException("Un utilisateur avec ce NAS existe déjà.");

            // 2. Mapping : Transformer le DTO en Entité Domain
            var user = new User
            {
                Nas = request.Nas,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PasswordHash = request.Password, // En vrai, on hacherait le mot de passe ici
                DateOfBirth = request.DateOfBirth,
                CreatedAt = DateTime.UtcNow
            };

            // 3. Persistance : Sauvegarder via le Repository
            await _userRepo.AddAsync(user);

            return user.Id;
        }
    }
}
