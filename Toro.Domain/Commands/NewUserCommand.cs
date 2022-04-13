using System.ComponentModel.DataAnnotations;

namespace Toro.Domain.Commands {

    /// <summary>
    /// Command para criação de um novo usuário
    /// </summary>
    public class NewUserCommand {     
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Password { get; set; }
    }
}
