using System.Threading.Tasks;
using Toro.Domain.Commands;

namespace Toro.Domain {
    public interface IEventRepository  {        
        Task<CommandResult> Order(EventCommand command);
    }
}
