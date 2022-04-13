﻿using System;

namespace Toro.Domain.Entity {
    public class Investor: To<int> {
        public string Cpf { get; private set; }
        public int Account { get; private set; }
        public int Branch { get; private set; }
        public string UserId { get; set; }
        public User User { get; set; }


        private Investor() {                 
        }

        public Investor(string cpf, int account, int branch) {           
            Cpf = cpf;
            Account = account;
            Branch = branch;          
        }

        public bool IsCpfEqual(string cpf) {
            return this.Cpf.Equals(cpf);
        }
    }
}
