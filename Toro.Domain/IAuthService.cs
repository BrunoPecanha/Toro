using System.Threading.Tasks;
using Toro.Domain.Commands;

namespace Toro.Domain {
    public interface IAuthService {
        public Task<CommandResult> Create(NewUserCommand command);
    }
}
