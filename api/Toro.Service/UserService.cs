using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Entity;

namespace Toro.Service {
    public class UserService : IUserService {
        private readonly IInvestorRepository _repositoryInvestor;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private const string msgSucessMsg = "Registrado com sucesso";
        private const string msgUserOrPassInvalid = "Login ou senha inválidos";
        private const string msgCpfAlreadyRegistered = "Este cpf já se encontra cadastrado";

        public UserService(IInvestorRepository repositoryInvestor, SignInManager<User> signInManager, UserManager<User> userManager) {
            _repositoryInvestor = repositoryInvestor;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        /// <summary>
        /// Registra um novo usuário
        /// </summary>
        /// <param name="command">Commando para criação de um novo usuário</param>
        /// <returns></returns>
        public async Task<CommandResult> Create(NewUserCommand command) {
            try {

                if (!await _repositoryInvestor.IsCpfAlreadyRegistered(command.Cpf)) {
                    var user = new User() {
                        UserName = command.Email,
                        Email = command.Email,
                        Cpf = command.Cpf,
                        EmailConfirmed = true,
                        Id = Guid.NewGuid().ToString(),
                        RegisteringDate = DateTime.Now
                    };

                    var result = await _userManager.CreateAsync(user, command.Password);
                    if (!result.Succeeded)
                        throw new Exception(string.Join(", ", result.Errors.Select(x => x.Description).ToArray()));

                    return new CommandResult(true, msgSucessMsg, result);
                }
                return new CommandResult(false, msgCpfAlreadyRegistered, null);
            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }

        /// <summary>
        /// Login de usuário
        /// </summary>
        /// <param name="command">Commando para login do usuário
        /// <returns></returns>
        public async Task<CommandResult> Login(LoginCommand command) {
            try {
                var ret = await _signInManager.PasswordSignInAsync(command.Email, command.Password, false, true);

                if (!ret.Succeeded) {
                    throw new Exception(msgUserOrPassInvalid);
                }
                var user = _userManager.FindByEmailAsync(command.Email);
                return new CommandResult(true, msgSucessMsg, new { id = user.Result.Id, name = user.Result.UserName });

            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }
    }
}
