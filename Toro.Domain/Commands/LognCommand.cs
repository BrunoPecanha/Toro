using System.ComponentModel.DataAnnotations;

namespace Toro.Domain.Commands {
    public class LognCommand {
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo {0} é obrigatório.")]
        public string Password { get; set; }
    }
}
