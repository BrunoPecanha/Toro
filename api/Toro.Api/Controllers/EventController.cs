using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Toro.Api.Dto;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Enum;

namespace Toro.Api.Controllers {
    [Authorize]
    [Route("event")]
    public class EventController : Controller {
        private readonly IEventService _service;
        private readonly IInvestorRepository _repository;

        public EventController(IEventService service, IInvestorRepository repository) {
            _service = service;
            _repository = repository;
        }

        /// <summary>
        /// Endpoint depósito de valores
        /// </summary>
        /// TORO-003 - Eu, como investidor, gostaria de poder depositar um valor na minha conta Toro, através de PiX ou TED bancária, para que eu possa realizar investimentos.
        [HttpPost]
        public async Task<IActionResult> OrderAsync([FromBody] EventDto dto) {
            if (!dto.IsValid()) {
                return BadRequest(new {
                    message = dto.ErroMessage,
                    valid = false
                });
            }

            var investor = await _repository.GetInvestorByUser(dto.UserId);

            if (investor is null && (EventEnum)dto.EventType == EventEnum.Buy) {
                return BadRequest(new {
                    message = "Usuário ainda não possível perfil de investidor. Faça o primeiro depósito para que seu perfil seja criado.",
                    valid = false
                });
            } else {
                var command = new EventCommand() {
                    InvestorId = investor is null ? 0 : investor.Id,
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
}
