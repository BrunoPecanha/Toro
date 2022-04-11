using System.Collections.Generic;

namespace Toro.Domain.Entity {
    public class Asset : To<string> { 
        public decimal CurrentPrice { get; private set; }
        public IList<AssetXPatrimony> AssetXPatrimony { get; set; }

        private Asset()  {
        }

        public Asset(string symbol,  decimal currentPrice) {
            Id = symbol;            
            CurrentPrice = currentPrice;
        }
    }
}
