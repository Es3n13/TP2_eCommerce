using System;
using System.Threading.Tasks;
using TP2.Domain.Interfaces;

namespace TP2.Infrastructure.Services
{
    public class CanadaRevenueService : ICanadaRevenueService
    {
        public async Task<decimal> GetOfficialIncomeAsync(int userId)
        {
            await Task.Delay(500);

            // SIMULATION DE PANNE : 30% de chance que l'API plante
            if (new Random().Next(1, 10) <= 3)
            {
                throw new Exception("ERREUR 500 : Le serveur de Revenu Canada est temporairement indisponible.");
            }
            return 25000m;
        }
    }
}
