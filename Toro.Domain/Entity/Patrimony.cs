using System.Collections.Generic;

namespace Toro.Domain.Entity {
    public class Patrimony : To<int> {

        public int InvestorId { get; private set; }
        public Investor Investor { get; private set; }
        public decimal AccountAmount { get; private set; }
        public IList<AssetXPatrimony> AssetXPatrimony { get; set; }

        private Patrimony() {
        }

        public Patrimony(Investor investor, decimal accountAmount, IList<AssetXPatrimony> assets) {
            Investor = investor;
            AccountAmount = accountAmount;
            AssetXPatrimony = assets;
        }

        public void UpdateAmount(decimal value) {
            this.AccountAmount += value;
        }
    }
}
