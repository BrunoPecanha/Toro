﻿using Microsoft.EntityFrameworkCore;
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
        private const string noMatchingInfo = "Não foi encontrado o investidor correspondente";
        private const string spbFailure = "Falha ao notificar o BC sobre a transação";
        private const string noEnoughCash = "Sem saldo suficiente para essa operação";

        public EventService(IToroContext dbContext) {
            _dbContext = dbContext;
        }

        public async Task<CommandResult> OrderAsync(EventCommand command) {

            try {

                User user = _dbContext.User.Where(x => x.Cpf == command.Cpf).FirstOrDefault();
                if (user is null) {
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

                    investorPatrimony = new Patrimony(investor, 0);
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
                    _dbContext.SaveChanges();

                    return new CommandResult(true, string.Empty, investorPatrimony);

                } else {
                    var asset = _dbContext.Asset.Where(x => x.Id == command.AssetId).FirstOrDefault();

                    if (investorPatrimony.IsThereBalanceForPurchase(asset.CurrentPrice * command.Amount)) {
                        _dbContext.AssetXPatrimony.Add(new AssetXPatrimony(investorPatrimony.Id, command.AssetId, (int)command.Amount));
                    } else
                        throw new Exception(noEnoughCash);
                }

                _dbContext.SaveChanges();

                return new CommandResult(true, string.Empty, null);
            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }
    }
}
