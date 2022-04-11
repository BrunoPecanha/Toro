using System.Threading.Tasks;
using Toro.Domain.Commands;

namespace Toro.Domain {
    public interface IEventService {
        public Task<CommandResult> Order(EventCommand command);       
    }
}
