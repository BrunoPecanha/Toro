using System;

namespace Toro.Domain.Entity {
    public class To {
        /// <summary>
        /// Id do registro
        /// </summary>       
        public int Id { get; private set; }
        /// <summary>
        /// Data de cadastro do registro.
        /// </summary>
        public DateTime RegisteringDate { get; private set; }      
    }
}
