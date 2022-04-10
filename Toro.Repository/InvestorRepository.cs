using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Toro.Domain;
using Toro.Domain.Commands;
using Toro.Repository.Context;

namespace Toro.Repository {
    public class InvestorRepository : IInvestorRepository {
        private readonly IToroContext _dbContext;
        private const string returnMessage = "Não foi encontrado nenhum patrimonio para o investidor informado";

        public InvestorRepository(IToroContext dbContext) {
            _dbContext = dbContext;
        }

        //TORO-002 - Eu, como investidor, gostaria de visualizar meu saldo, meus investimentos e meu patrimônio total na Toro.
        public async Task<CommandResult> GetBalanceByIdAsync(int investorId) {
            try {
                var patrimony = await _dbContext.Patrimony
                                          .Include(x => x.Investor)
                                          .Include(x => x.AssetXPatrimony)
                                          .ThenInclude(x => x.Asset)
                                          .Where(x => x.InvestorId == investorId)
                                          .AsNoTracking()
                                          .FirstOrDefaultAsync();

                if (patrimony is null) {
                    throw new Exception(returnMessage);
                }

                return new CommandResult(true, string.Empty, patrimony);

            } catch (Exception ex) {
                return new CommandResult(false, ex.Message, null);
            }
        }

        public Task<CommandResult> GetTrendsAsync() {
            throw new NotImplementedException();
        }
    }
}
