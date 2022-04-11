using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Toro.Api.Dto;
using Toro.Domain.Enum;
using Toro.Domain;
using Toro.Domain.Commands;
using System;

namespace Toro.Api.Controllers {
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
        public async Task<IActionResult> Order([FromBody] EventDto dto) {

            var command = new EventCommand() {
                Amount = Int32.Parse(dto.Amount),
                AssetId = dto.AssetId,
                Cpf = dto.Cpf,
                EventType = (EventEnum)Int32.Parse(dto.EventType),
                OriginBank = Int32.Parse(dto.OriginBank),
                OriginBranch = Int32.Parse(dto.OriginBranch)        
            };

            var ret = await _service.Order(command);

            if (ret.Valid)
                return Ok(ret);

            return BadRequest(ret);
        }    
    }
}
