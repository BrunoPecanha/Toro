using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;

namespace Toro.Api.Controllers {
    [Route("api/investor")]
    public class EventController : Controller {
        private readonly IInvestorRepository _repository;

        public EventController(IInvestorRepository repository) {
            _repository = repository;
        }

        /// <summary>
        /// Endpoint para recuperação do saldo do investidor
        /// </summary>
        /// TORO-002 - Eu, como investidor, gostaria de visualizar meu saldo, meus investimentos e meu patrimônio total na Toro.
        [HttpPost("userPosition")]
        public async Task<IActionResult> Order([FromBody] EventCommand command) {
            //var ret = await _repository.GetBalanceByIdAsync(id);

            //if (ret.Valid)
            //    return Ok(ret);

          return BadRequest();
        }      

    }
}
