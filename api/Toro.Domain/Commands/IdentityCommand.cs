using System.ComponentModel.DataAnnotations;

namespace Toro.Domain.Commands {
    public class RegisterCommand {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Password { get; set; }
    }

    public class LoginCommand {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Password { get; set; }
    }
}
