using System.Threading.Tasks;
using Toro.Domain.Commands;

namespace Toro.Domain {
    public interface IInvestorRepository  {
        Task<CommandResult> GetBalanceByIdAsync(int investorId);
        Task<CommandResult> GetTrendsAsync();
    }
}
