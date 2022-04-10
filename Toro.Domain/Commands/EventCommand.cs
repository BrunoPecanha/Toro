using Toro.Domain.Enum;

namespace Toro.Domain.Commands {

    /// <summary>
    /// Command para depósitos ou compra de ações
    /// </summary>
    public class EventCommand {
        public int UserId { get; set; }
        public int AssetId { get; set; }
        public int OriginBank { get; set; }
        public int OriginBranch { get; set; }
        public int Cpf { get; set; }
        public decimal Amount { get; set; }
        public EventEnum EventType { get; set; }        
    }
}
