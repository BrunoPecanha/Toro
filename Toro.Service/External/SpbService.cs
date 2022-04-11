using Toro.Domain.Commands;

namespace Toro.Service.External {
    public static class SpbService {

        public static bool NotifySpb(EventCommand command) {
            // Lógica para envio da notificação ao SBP do Banco Central.
            return true;
        }
    }
}

