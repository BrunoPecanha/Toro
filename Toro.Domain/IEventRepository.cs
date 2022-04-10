using System.Threading.Tasks;
using Toro.Domain.Commands;

namespace Toro.Domain {
    public interface IEventRepository  {
        Task<CommandResult> Deposit(EventCommand command);
        Task<CommandResult> Order(EventCommand command);
    }
}
