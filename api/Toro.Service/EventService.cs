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
        private const string noMatchingInfo = "Depósitos só podem ser feitos para o mesmo CPF desta conta.";
        private const string spbFailure = "Falha ao notificar o BC sobre a transação";
        private const string noEnoughCash = "Sem saldo suficiente para essa operação";
        private const string sucessedBought = "Ativo adquirido com sucesso!";
        private const string amountDepositedSucessed = "Depósito realizado com sucesso!";

        public EventService(IToroContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> OrderAsync(EventCommand command) {

            try {
                User user = _dbContext.User.Where(x => x.Cpf == command.Cpf).FirstOrDefault();

                if (command.EventType == EventEnum.Transfer && user is null) {
                    throw new Exception(noMatchingInfo);
                }

                Patrimony investorPatrimony = _dbContext.Patrimony
                                              .Include(x => x.Investor)
                                              .Include(x => x.AssetXPatrimony)
                                              .Where(x => x.Investor.Id == command.InvestorId)
                                              .FirstOrDefault();
                if (investorPatrimony is null) {
                    Investor investor = _dbContext.Investor
                                                  .Where(x => x.Cpf == command.Cpf)
                                                  .FirstOrDefault();

                    if (investor is null) {
                        investor = new Investor(user);
                    }

                    investorPatrimony = new Patrimony(investor, command.Amount);
                    _dbContext.Patrimony.Add(investorPatrimony);
                }

                if (command.EventType == EventEnum.Transfer) {
                    if (investorPatrimony is null || !investorPatrimony.Investor.IsCpfEqual(command.Cpf)) {
                        throw new Exception(noMatchingInfo);
                    }

                    if (!SpbService.NotifySpb(command)) {
                        throw new Exception(spbFailure);
                    }

                    investorPatrimony.UpdateAmount(command.Amount);
                    await _dbContext.SaveChangesAsync();

                    return new CommandResult(true, amountDepositedSucessed, investorPatrimony);

                } else {
                    var asset = _dbContext.Asset.Where(x => x.Id == command.AssetId).FirstOrDefault();

                    if (investorPatrimony.IsThereBalanceForPurchase(asset.CurrentPrice * command.Amount)) {
                        _dbContext.AssetXPatrimony.Add(new AssetXPatrimony(investorPatrimony.Id, command.AssetId, (int)command.Amount));
                    } else
                        throw new Exception(noEnoughCash);
                }

                await _dbContext.SaveChangesAsync();

                return new CommandResult(true, sucessedBought, null);
            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }
    }
}
