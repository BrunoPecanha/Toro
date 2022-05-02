using Toro.Domain.Entity;
using Xunit;

namespace Toro.Teste {
    public class ToroTest {

        [Fact]
        [Trait("Cpf", "11111111111")]
        [Trait("UserName", "kratos@godofwar.com")]
        [Trait("Email", "kratos@godofwar.com")]
        public void OrderAsync_Should_Complete_Deposit_Investor_Account() {
            //Arrange
            User user = new User("kratos@godofwar.com", "kratos@godofwar.com", "11111111111");
            var investor = new Investor(user);
            //Act
            var isTrue = investor.IsCpfEqual("11111111111");
            // Assert
            Assert.True(isTrue);
        }

        [Fact]
        [Trait("Cpf", "22222222222")]
        [Trait("UserName", "mickeymouse@mickeymouse.com")]
        [Trait("Email", "mickeymouse@godofwar.com")]
        public void OrderAsync_Should_Not_Complete_Deposit_Investor_Account_CPF_Different() {
            User user = new User("mickeymouse@mickeymouse.com", "mickeymouse@mickeymouse.com", "22222222222");
            //Arrange
            var investor = new Investor(user);
            //Act
            var isTrue = investor.IsCpfEqual("13676616766");
            // Assert
            Assert.False(isTrue);
        }

        [Fact]
        [Trait("Amount", "32")]
        [Trait("Cpf", "22222222222")]
        [Trait("UserName", "mickeymouse@mickeymouse.com")]
        public void OrderAsync_Should_Complete_Purchase_Enough_Cash() {
            //Arrange
            Patrimony patrimony = new Patrimony(new Investor(new User("mickeymouse@mickeymouse.com", "mickeymouse@mickeymouse.com", "22222222222")), 100);
            //Act
            var validPurchase = patrimony.IsThereBalanceForPurchase(32);           
            // Assert
            Assert.True(validPurchase);
        }

        [Fact]
        [Trait("Amount", "32")]
        [Trait("Cpf", "11111111111")]
        [Trait("UserName", "mickeymouse@mickeymouse.com")]
        public void OrderAsync_Should_Not_Complete_Purchase_Not_Enough_Cash() {
            //Arrange
            Patrimony patrimony = new Patrimony(new Investor(new User("kratos@godofwar.com", "kratos@godofwar.com", "11111111111")), 10);
            //Act
            var validPurchase = patrimony.IsThereBalanceForPurchase(32);
            // Assert
            Assert.False(validPurchase);
        }
    }
}
