using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Toro.Api.Dto;
using Toro.Domain.Enum;
using Toro.Domain;
using Toro.Domain.Commands;
using System;
using Microsoft.AspNetCore.Authorization;

namespace Toro.Api.Controllers {
   // [Authorize]
    [Route("event")]
    public class EventController : Controller {
        private readonly IEventService _service;

        public EventController(IEventService service) {
            _service = service;
        }

        /// <summary>
        /// Endpoint depósito de valores
        /// </summary>
        /// TORO-003 - Eu, como investidor, gostaria de poder depositar um valor na minha conta Toro, através de PiX ou TED bancária, para que eu possa realizar investimentos.
        [HttpPost]
        public async Task<IActionResult> OrderAsync([FromBody] EventDto dto) {
            if (!dto.IsValid()) {
                return BadRequest(dto.ErroMessage);
            }


            var command = new EventCommand() {
                InvestorId = dto.InvestorId,
                Amount = dto.Amount,
                AssetId = dto.AssetId,
                Cpf = dto.Cpf,
                EventType = (EventEnum)dto.EventType,
                OriginBank = dto.OriginBank,
                OriginBranch = dto.OriginBranch
            };

            var ret = await _service.OrderAsync(command);

            if (ret.Valid)
                return Ok(ret);

            return BadRequest(ret);
        }    
    }
}
