using Toro.Domain.Enum;

namespace Toro.Domain.Commands {

    /// <summary>
    /// Command para depósitos ou compra de ações
    /// </summary>
    public class EventCommand {
        public int InvestorId { get; set; }
        public string AssetId { get; set; }
        public int OriginBank { get; set; }
        public int OriginBranch { get; set; }
        public string Cpf { get; set; }
        public int Amount { get; set; }
        public EventEnum EventType { get; set; }        
    }
}
