using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Toro.Domain;

namespace Toro.Api.Controllers {
    //[Authorize]
    [Route("investor")]
    public class InvestorController : Controller {
        private readonly IInvestorRepository _repository;

        public InvestorController(IInvestorRepository repository) {
            _repository = repository;
        }

        /// <summary>
        /// Endpoint para recuperação do saldo do investidor
        /// </summary>
        /// TORO-002 - Eu, como investidor, gostaria de visualizar meu saldo, meus investimentos e meu patrimônio total na Toro.
        [HttpGet("userPosition")]
        public async Task<IActionResult> GetBalanceByIdAsync([FromQuery] string id) {
            var ret = await _repository.GetBalanceByIdAsync(id);

            if (ret.Valid)
                return Ok(ret);

            return BadRequest(ret);
        }

        /// <summary>
        /// Endpoint que trás os ativos mais negociados nos últimos 5 dias
        /// </summary>
        /// TORO-004 - Eu, como investidor, gostaria de ter acesso a uma lista de 5 ações mais negociadas nos últimos 7 dias, com seus respectivos preços.
        [HttpGet]
        public async Task<IActionResult> GetTrendsAsync() {
            var ret = await _repository.GetTrendsAsync();

            if (ret.Valid)
                return Ok(ret);

            return BadRequest(ret);
        }
    }
}
