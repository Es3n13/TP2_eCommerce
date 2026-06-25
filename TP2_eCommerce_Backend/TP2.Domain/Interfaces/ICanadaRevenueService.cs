using System.Threading.Tasks;

namespace TP2.Domain.Interfaces
{
    public interface ICanadaRevenueService
    {
        // Retourne le montant officiel enregistré chez Revenu Canada pour cet utilisateur
        Task<decimal> GetOfficialIncomeAsync(int userId);
    }

}
