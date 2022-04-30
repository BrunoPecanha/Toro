using Microsoft.AspNetCore.Identity;

namespace Toro.Service {
    public class IdentityErrorDescriberService : IdentityErrorDescriber {
        public override IdentityError DuplicateEmail(string email) {
            return new IdentityError {
                Code = nameof(DuplicateEmail),
                Description = "E-mail já cadastrado."
            };
        }

        public override IdentityError DuplicateUserName(string userName) {
            return new IdentityError {
                Code = nameof(DuplicateUserName),
                Description = "E-mail já cadastrado."
            };
        }

        public override IdentityError InvalidEmail(string email) {
            return new IdentityError {
                Code = nameof(InvalidEmail),
                Description = "E-mail inválido."
            };
        }

        public override IdentityError InvalidToken() {
            return new IdentityError {
                Code = nameof(InvalidToken),
                Description = "O token não é válido."
            };
        }

        public override IdentityError InvalidUserName(string userName) {
            return new IdentityError {
                Code = nameof(InvalidUserName),
                Description = "Nome de usuário inválido."
            };
        }


        public override IdentityError PasswordMismatch() {
            return new IdentityError {
                Code = nameof(PasswordMismatch),
                Description = "Senha inválida"
            };
        }

        public override IdentityError PasswordRequiresDigit() {
            return new IdentityError {
                Code = nameof(PasswordRequiresDigit),
                Description = "Senha precisa de um número ao menos"
            };
        }

        public override IdentityError PasswordRequiresLower() {
            return new IdentityError {
                Code = nameof(PasswordRequiresLower),
                Description = "Senha precisa de uma letra minúscula ao menos"
            };
        }

        public override IdentityError PasswordRequiresNonAlphanumeric() {
            return new IdentityError {
                Code = nameof(PasswordRequiresNonAlphanumeric),
                Description = "Senha precisa de um caracter especial ao menos"
            };
        }

        public override IdentityError PasswordRequiresUniqueChars(int uniqueChars) {
            return new IdentityError {
                Code = nameof(PasswordRequiresUniqueChars),
                Description = "Não entendi isso, vou pesquisar"
            };
        }

        public override IdentityError PasswordRequiresUpper() {
            return new IdentityError {
                Code = nameof(PasswordRequiresUpper),
                Description = "Senha precisa de uma letra maiúscula ao menos"
            };
        }

        public override IdentityError PasswordTooShort(int length) {
            return new IdentityError {
                Code = nameof(PasswordTooShort),
                Description = "Senha muito curta"
            };
        }
    }
}
