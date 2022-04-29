using System.Threading.Tasks;
using Toro.Domain.Commands;
using Toro.Domain.Entity;

namespace Toro.Domain {
    public interface IInvestorRepository : IRepositoryBase<Investor> {
        Task<CommandResult> GetBalanceByIdAsync(string userId);
        Task<CommandResult> GetTrendsAsync();
        Task<Investor> GetInvestorByUser(string id);
    }
}
