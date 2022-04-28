using System.ComponentModel.DataAnnotations;

namespace Toro.Domain.Commands {
    public class RegisterDto {
        public string Cpf { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Password { get; set; }
    }

    public class LoginDto {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Password { get; set; }
    }
}
