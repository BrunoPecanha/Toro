using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Entity;
using System.Linq;

namespace Toro.Service {
    public class AuthService : IAuthService {
        private readonly IInvestorRepository _repositoryInvestor;
        private readonly SignInManager<User> _signInManager;
        private const string sucessMsg = "Registrado com sucesso";

        public AuthService(IInvestorRepository repositoryInvestor, SignInManager<User> signInManager) {
            _repositoryInvestor = repositoryInvestor;
            _signInManager = signInManager;        
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="command">Commando para criação de um novo usuário</param>
        /// <returns></returns>
        public async Task<CommandResult> Create(NewUserCommand command) {
            try {
                var user = new User() {
                    UserName = command.Email,
                    Email = command.Email,
                    Cpf = command.Cpf,
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString(),
                    RegisteringDate = DateTime.Now
                };
                _repositoryInvestor.Add(new Investor(user));

                return new CommandResult(true, sucessMsg, null);
            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }
    }
}
