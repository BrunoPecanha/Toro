using System.ComponentModel.DataAnnotations;
using Toro.Domain.Enum;

namespace Toro.Api.Dto {
    public class EventDto {
        public int InvestorId { get; set; }
        public string AssetId { get; set; }
        public int OriginBank { get; set; }
        public int OriginBranch { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public int Amount { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public int EventType { get; set; }
        public string ErroMessage { get; set; }

        public bool IsValid() {
            bool valid = true;
            if (string.IsNullOrEmpty(this.Cpf)) {
                this.ErroMessage = "CPF obrigatório ";
                valid = false;
            } else if ((EventEnum)this.EventType == EventEnum.Transfer) {
                if (OriginBank <= 0 || OriginBranch <= 0 || Amount <= 0) {
                    valid = false;
                    ErroMessage = "Banco, conta e quantidade devem estar preenchidos com valores maiores 0";
                }
            } else {
                if (string.IsNullOrEmpty(AssetId) || Amount <= 0) {
                    valid = false;
                    ErroMessage = "Identificador da ação inválido ou quantidade menor que 1";
                }  
            }
            return valid;
        }
    }
}
