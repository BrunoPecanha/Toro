using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Entity;
using Toro.Domain.Enum;
using Toro.Repository.Context;
using Toro.Service.External;

namespace Toro.Service {
    public class EventService : IEventService {
        private readonly IToroContext _dbContext;
        private const string noMatchingCpfInfo = "Cpf não correspondente";
        private const string spbFailure = "Falha ao notificar o BC sobre a transação";

        public EventService(IToroContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> Order(EventCommand command) {

            try {

                Patrimony investorPatrimony = _dbContext.Patrimony
                                              .Include(x => x.Investor)
                                              .Where(x => x.Investor.Cpf == command.Cpf)
                                              .FirstOrDefault();

                if (investorPatrimony is null) {
                    throw new Exception(noMatchingCpfInfo);
                }               

                if (command.EventType == EventEnum.Transfer) {
                    if (!SpbService.NotifySpb(command)) {
                        throw new Exception(spbFailure);
                    }

                    investorPatrimony.UpdateAmount(command.Amount);
                    _dbContext.SaveChanges();

                    return new CommandResult(true, string.Empty, investorPatrimony);

                } else {
                    _dbContext.AssetXPatrimony.Add(new AssetXPatrimony(investorPatrimony.Id, command.AssetId, command.Amount));
                    _dbContext.SaveChanges();

                    return new CommandResult(true, string.Empty, null);
                }
            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }
    }
}
