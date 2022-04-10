using System.Collections.Generic;

namespace Toro.Domain.Entity {
    public class Asset : To {
        public string Symbol { get; private set; }      
        public decimal CurrentPrice { get; private set; }
        public IList<AssetXPatrimony> AssetXPatrimony { get; set; }

        private Asset() {
        }

        public Asset(string symbol,  decimal currentPrice, int amount) {
            Symbol = symbol;            
            CurrentPrice = currentPrice;
        }
    }
}
