using System;

namespace Toro.Domain.Entity {
    public class To<T> {
        /// <summary>
        /// Id do registro
        /// </summary>       
        public T Id { get; protected set; }
        /// <summary>
        /// Data de cadastro do registro.
        /// </summary>
        public DateTime RegisteringDate { get; private set; }      
    }
}
