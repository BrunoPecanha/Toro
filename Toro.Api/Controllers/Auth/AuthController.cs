using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;

namespace ToroApi.Controllers.Auth {
    [Route("api/investor")]
    public class AuthController : Controller {
        private readonly IAuthService _service;

        public AuthController(IAuthService service) {
            _service = service;
        }

        /// <summary>
        /// Endpoint de cadastro de usuário
        /// </summary>
        [HttpPost("register")]
        public async Task<IActionResult> Create([FromBody] RegisterDto regDto) {
            var command = new NewUserCommand() {
                Email = regDto.Email,
                Cpf = regDto.Cpf,
                Password = regDto.Password
            };

            var ret = await _service.Create(command);

            if (!ret.Valid)
                return BadRequest(ret.Message);

            return Ok(ret.Log);
        }
    }
}
