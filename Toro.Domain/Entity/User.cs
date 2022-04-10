namespace Toro.Domain.Entity {
    public class User: To {
        public string Login { get; private set; }
        public string Password { get; private set; }
        public int InvestorId { get; set; }

        private User() {                
        }

        public User(string login, string password) {           
            Login = login;
            Password = password;          
        }
    }
}
