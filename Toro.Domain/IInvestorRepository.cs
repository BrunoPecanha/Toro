using System.Threading.Tasks;
using Toro.Domain.Commands;
using Toro.Domain.Entity;

namespace Toro.Domain {
    public interface IInvestorRepository  {
        Task<CommandResult> GetBalanceByIdAsync(int investorId);
        Task<CommandResult> GetTrendsAsync();
    }
}
