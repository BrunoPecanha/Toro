using System.Collections.Generic;

namespace Toro.Domain.Entity {
    public class Patrimony : To<int> {

        public int InvestorId { get; private set; }
        public Investor Investor { get; private set; }
        public decimal AccountAmount { get; private set; }
        public IList<AssetXPatrimony> AssetXPatrimony { get; private set; }

        private Patrimony() {
        }

        public Patrimony(Investor investor, decimal amount) {
            this.Investor = investor;
            investor.PatrimonyId = this.Id;
            this.AccountAmount = amount;
            this.AssetXPatrimony = new List<AssetXPatrimony>();
        }

        public void UpdateAmount(decimal amount) {
            this.AccountAmount += amount;
        }

        public bool IsThereBalanceForPurchase(decimal purchaseValue) {
            if (AccountAmount >= purchaseValue) {
                this.AccountAmount -= purchaseValue;
                return true;
            }

            return false;
        }

    }
}
