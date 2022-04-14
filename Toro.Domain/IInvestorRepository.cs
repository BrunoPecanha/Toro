using System.Threading.Tasks;
using Toro.Domain.Commands;
using Toro.Domain.Entity;

namespace Toro.Domain {
    public interface IInvestorRepository : IRepositoryBase<Investor> {
        Task<CommandResult> GetBalanceByIdAsync(int investorId);
        Task<CommandResult> GetTrendsAsync();
        Task<CommandResult> GetInvestorByCPF(string cpf);
    }
}
