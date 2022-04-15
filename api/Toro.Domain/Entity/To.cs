using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Toro.Domain.Entity {
    public class To<T> {
        /// <summary>
        /// Id do registro
        /// </summary>       
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public T Id { get; protected set; }
        /// <summary>
        /// Data de cadastro do registro.
        /// </summary>
        public DateTime RegisteringDate { get; private set; }
        /// <summary>
        /// Ultima atualização do registro
        /// </summary>
        public DateTime LastUpdate { get; set; }
    }
}
