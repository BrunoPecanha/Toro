namespace Toro.Domain.Commands {
    public class CommandResult {

        public CommandResult(bool valid, string message, object data) {
            this.Valid = valid;
            this.Message = message;
            this.Data = data;
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

        public object Data { get; set; }

    }
}
