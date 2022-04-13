using Microsoft.AspNetCore.Identity;
using System;

namespace Toro.Domain.Entity {
    public class User: IdentityUser<string> {
        public string Cpf { get; set; }
        public int InvestorId { get; set; }
        public DateTime RegisteringDate { get; set; }
    }
}
