namespace Toro.Domain.Entity {
    public class AssetXPatrimony : To<int>{
        public int PatrimonyId { get; private set; }
        public Patrimony Patrimony { get; private set; }
        public string AssetId { get; private set; }
        public Asset Asset { get; private set; }
        public int Amount { get; private set; }        

        private AssetXPatrimony() {
        }

        public AssetXPatrimony(int patrimonyId, string assetId, int amount) {
            PatrimonyId = patrimonyId;
            AssetId = assetId;
            Amount = amount;                
        }
    }
}
