using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Domain.Entity;
using Toro.Repository.Context;

namespace Toro.Repository {
    public class InvestorRepository : RepositoryBase<Investor>, IInvestorRepository {
        private readonly IToroContext _dbContext;
        private const string erromsg = "Não foi encontrado nenhum ativo na sua carteira de investimentos";
        private const string noAssetinfo = "Não houveram transações nos últimos 7 dias";
        private const string naoHaAtivos = "Não há dados de investidor para esse perfil.";

        public InvestorRepository(IToroContext dbContext) {
            _dbContext = dbContext;
        }

        //TORO-002 - Eu, como investidor, gostaria de visualizar meu saldo, meus investimentos e meu patrimônio total na Toro.
        public async Task<CommandResult> GetBalanceByIdAsync(string userId) {
            try {

                var investor = await this.GetInvestorByUser(userId);

                if (investor is null)
                    return new CommandResult(true, naoHaAtivos, null);

                var query = await _dbContext.Patrimony
                                          .Include(x => x.Investor)
                                          .Include(x => x.AssetXPatrimony)
                                          .ThenInclude(x => x.Asset)
                                          .Where(x => x.Investor.Id == investor.Id)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync();

                if (query is null) {
                    return new CommandResult(true, erromsg, null);
                }

                var assets = query.AssetXPatrimony                                  
                                  .Select(x => new {
                                      Symbol = x.AssetId,
                                      Amount = x.Amount,
                                      OperationDate = x.RegisteringDate,
                                      CurrentPrince = _dbContext.Asset.Where(y => x.AssetId == y.Id)
                                  .FirstOrDefault().CurrentPrice
                                  })                                  
                                  .OrderBy(x => x.Symbol)
                                  .ToList();
                                 

                var patrimony = new {
                    AccountAmount = query.AccountAmount,
                    Assets = assets,
                    TotalAmount = query.AccountAmount + assets.Sum(x => x.CurrentPrince * x.Amount)
                };

                return new CommandResult(true, string.Empty, patrimony);

            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }

        //TORO-004 - Eu, como investidor, gostaria de ter acesso a uma lista de 5 ações mais negociadas nos últimos 7 dias, com seus respectivos preços
        public async Task<CommandResult> GetTrendsAsync() {
            try {
                var query = await _dbContext.AssetXPatrimony
                                        .Include(x => x.Asset)
                                        .AsNoTracking()
                                        .Where(x => x.LastUpdate > DateTime.Now.AddDays(-7))
                                        .Select(x => x.Asset)                                      
                                        .Take(5)
                                        .ToArrayAsync();
                                        
                                        

                var trends = query.GroupBy(x => new { x.Id, x.CurrentPrice})
                      .Select(x => new { symbol = x.Key.Id, price = x.Key.CurrentPrice, qt = x.Count() })
                      .OrderByDescending(x => x.qt);


                if (trends is null || trends.Count() < 1) {
                    throw new Exception(noAssetinfo);
                }

                return new CommandResult(true, string.Empty, trends);

            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }      

        public async Task<Investor> GetInvestorByUser(string id) {
            return await _dbContext.Investor
                                        .Include(x => x.User)
                                        .FirstOrDefaultAsync(x => x.UserId.Equals(id));
        }
    }
}
