using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Toro.Domain;

namespace ToroApi.Controllers.Auth {
    [Route("api/investor")]
    public class AuthController : Controller {
        private readonly IInvestorRepository _repository;

        public AuthController(IInvestorRepository repository) {
            _repository = repository;
        }

        /// <summary>
        /// Endpoint para recuperação do saldo do investidor
        /// </summary>
        /// TORO-002 - Eu, como investidor, gostaria de visualizar meu saldo, meus investimentos e meu patrimônio total na Toro.
        [HttpGet("login")]
        public async Task<IActionResult> Login([FromBody] string user, string password) {
            //var ret = await _repository.GetBalanceByIdAsync(id);

            //if (ret.Valid)
            //    return Ok(ret);

            //return BadRequest(ret);
            return null;
        }
    }
}
