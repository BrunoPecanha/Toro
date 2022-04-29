using System.ComponentModel.DataAnnotations;
using Toro.Domain.Enum;

namespace Toro.Api.Dto {
    public class EventDto {
        public string UserId { get; set; }
        public string AssetId { get; set; }
        public int OriginBank { get; set; }
        public int OriginBranch { get; set; }
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public int EventType { get; set; }
        public string ErroMessage { get; set; }

        public bool IsValid() {
            bool valid = true;
            if ((EventEnum)this.EventType == EventEnum.Transfer && string.IsNullOrEmpty(this.Cpf)) {
                this.ErroMessage = "CPF obrigatório ";
                valid = false;
            } else if ((EventEnum)this.EventType == EventEnum.Transfer) {
                if (OriginBank <= 0 || OriginBranch <= 0 || Amount <= 0) {
                    valid = false;
                    ErroMessage = "Banco, conta e quantidade devem estar preenchidos com valores maiores 0";
                }
            } else {
                if ((EventEnum)this.EventType == EventEnum.Buy && Amount <= 0) {
                    valid = false;
                    ErroMessage = "Quantidade não pode ser menor que 1";
                }  
            }
            return valid;
        }
    }
}
