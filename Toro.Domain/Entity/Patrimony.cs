using System.Collections.Generic;

namespace Toro.Domain.Entity {
    public class Patrimony : To<int> {

        public int InvestorId { get; private set; }
        public Investor Investor { get; private set; }
        public decimal AccountAmount { get { return AccountAmount; } set { this.AccountAmount += value; } }
        public IList<AssetXPatrimony> AssetXPatrimony { get; set; }

        private Patrimony() {
        }
    }
}
