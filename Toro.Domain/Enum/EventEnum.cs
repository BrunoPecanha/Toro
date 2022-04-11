using System.ComponentModel;

namespace Toro.Domain.Enum {
    public enum EventEnum  {
        [Description("Transfer")]
        Transfer = 0,
        [Description("Buy")]
        Buy = 1
    }
}
