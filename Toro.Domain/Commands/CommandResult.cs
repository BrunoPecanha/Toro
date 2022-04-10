namespace Toro.Domain.Commands {
    public class CommandResult {

        public CommandResult(bool valid, string messege, object log) {
            this.Valid = valid;
            this.Message = messege;
            this.Log = log;
        }
        /// Indica se a operação foi validada
        /// </summary>

        public bool Valid { get; set; }       
        /// <summary>
        /// Messagem Padrão de saída
        /// </summary>

        public string Message { get; set; }
        /// <summary>
        /// Objeto de saída
        /// </summary>

        public object Log { get; set; }

    }
}
