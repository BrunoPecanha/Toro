using System;

namespace Toro.Domain.Entity {
    public class AssetXPatrimony {
        public int PatrimonyId { get; set; }
        public Patrimony Patrimony { get; set; }
        public string AssetId { get; set; }
        public Asset Asset { get; set; }
        public int Amount { get; set; }
        public DateTime RegisteringDate { get; private set; }

        private AssetXPatrimony() {
        }

        public AssetXPatrimony(int patrimonyId, string assetId, int amount) {
            PatrimonyId = patrimonyId;
            AssetId = assetId;
            Amount = amount;                
        }
    }
}
