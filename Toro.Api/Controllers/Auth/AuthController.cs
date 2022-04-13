using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Entity;

namespace ToroApi.Controllers.Auth {
    [Route("api/investor")]
    public class AuthController : Controller {
        private readonly IInvestorRepository _repository;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private const string sucessMsg = "Registrado com sucesso";

        public AuthController(IInvestorRepository repository, SignInManager<User> signInManager, UserManager<User> userManager) {
            _signInManager = signInManager;
            _userManager = userManager;
            _repository = repository;
        }

        /// <summary>
        /// Endpoint de cadastro de usuário
        /// </summary>
        /// TORO-002 - Eu, como investidor, gostaria de visualizar meu saldo, meus investimentos e meu patrimônio total na Toro.
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto regDto) {
            var user = new User() {
                UserName = regDto.Name,
                Email = regDto.Email,
                Cpf = regDto.Cpf,
                EmailConfirmed = true,
                Id = Guid.NewGuid().ToString(),
                RegisteringDate = DateTime.Now
            };

            var ret = await _userManager.CreateAsync(user, regDto.Password);

            if (!ret.Succeeded)
                return BadRequest(ret.Errors);

            return Ok(sucessMsg);
        }
    }
}
