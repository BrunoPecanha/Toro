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
        private const string noMatchingCpfInfo = "Não foi encontrado o investidor correspondente";
        private const string spbFailure = "Falha ao notificar o BC sobre a transação";

        public EventService(IToroContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> OrderAsync(EventCommand command) {

            try {

                Patrimony investorPatrimony = _dbContext.Patrimony
                                              .Include(x => x.Investor)
                                              .Include(x => x.AssetXPatrimony)
                                              .Where(x => x.Investor.Id == command.InvestorId)
                                              .FirstOrDefault();

                if (command.EventType == EventEnum.Transfer) {

                    if (investorPatrimony is null || !investorPatrimony.Investor.IsCpfEqual(command.Cpf)) {
                        throw new Exception(noMatchingCpfInfo);
                    }

                    if (!SpbService.NotifySpb(command)) {
                        throw new Exception(spbFailure);
                    }

                    investorPatrimony.AccountAmount = command.Amount;
                    _dbContext.SaveChanges();

                    return new CommandResult(true, string.Empty, investorPatrimony);

                } else {

                    if (_dbContext.AssetXPatrimony.Any(x => x.AssetId.Equals(command.AssetId))) {
                        var asset = investorPatrimony.AssetXPatrimony.Where(x => x.AssetId == command.AssetId).FirstOrDefault();
                        asset.UpdateAmount(command.Amount);
                    } else
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
