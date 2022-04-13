using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Entity;
using Toro.Repository.Context;

namespace Toro.Service {
    public class AuthService : IAuthService {
        private readonly IToroContext _toroContext;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private const string sucessMsg = "Registrado com sucesso";

        public AuthService(IToroContext toroContext, SignInManager<User> signInManager, UserManager<User> userManager) {
            _toroContext = toroContext;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="command">Commando para criação de um novo usuário</param>
        /// <returns></returns>
        public async Task<CommandResult> Create(NewUserCommand command) {
            // Aqui poderia entrar numa validação para saber se o usuário já existe, mas não foi gerado um perfil de investidor, 
            // antes de salvar novamente, caso uma tentativa anterior tenha sido feita, criado o usuário, mas o perfil de investidor não.
            try {
                var user = new User() {
                    UserName = command.Email,
                    Email = command.Email,
                    Cpf = command.Cpf,
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString(),
                    RegisteringDate = DateTime.Now
                };
               
                var ret = await _userManager.CreateAsync(user);

                if (!ret.Succeeded)
                    throw new Exception(string.Join("; ", ret.Errors));
                else {
                    Investor investor = new Investor(user);

                    _toroContext.Investor.Add(investor);
                    _toroContext.SaveChanges();
                }

                return new CommandResult(true, string.Empty, ret);
            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }
    }
}
