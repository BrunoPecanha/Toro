using Microsoft.AspNetCore.Identity;
using System;

namespace Toro.Domain.Entity {
    public class User: IdentityUser<string> {
        public string Cpf { get; set; }
        public DateTime RegisteringDate { get; set; }
        public DateTime LastUpdate { get; set; }
        public Investor Investor { get; set; }

        public User(string userName, string email, string cpf): base(userName) {
            this.UserName = userName;
            this.Cpf = cpf;
            this.Email = email;
        }
    }
}
