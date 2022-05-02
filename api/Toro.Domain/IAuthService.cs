using System.Threading.Tasks;
using Toro.Domain.Commands;

namespace Toro.Domain {
    public interface IUserService {
        public Task<CommandResult> Create(NewUserCommand command);
        public Task<CommandResult> Login(LoginCommand command);
    }
}
