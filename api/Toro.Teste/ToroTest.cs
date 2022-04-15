using Toro.Domain.Entity;
using Xunit;

namespace Toro.Teste {
    public class ToroTest {

        [Fact]
        [Trait("Cpf", "11111111111")]
        public void OrderAsync_Should_Complete_Deposit_Investor_Account() {
            //Arrange
            User user = new User() {
                Cpf = "11111111111"
            };
            var investor = new Investor(user);
            //Act
            var isTrue = investor.IsCpfEqual("11111111111");
            // Assert
            Assert.True(isTrue);
        }

        [Fact]
        [Trait("Cpf", "22222222222")]
        public void OrderAsync_Should_Not_Complete_Deposit_Investor_Account_CPF_Different() {
            User user = new User() {
                Cpf = "11111111111"
            };
            //Arrange
            var investor = new Investor(user);
            //Act
            var isTrue = investor.IsCpfEqual("22222222222");
            // Assert
            Assert.False(isTrue);
        }

        [Fact]
        [Trait("Amount", "32")]
        public void OrderAsync_Should_Complete_Purchase_Enough_Cash() {
            //Arrange
            Patrimony patrimony = new Patrimony(new Investor(new User()), 100);
            //Act
            var validPurchase = patrimony.IsThereBalanceForPurchase(32);           
            // Assert
            Assert.True(validPurchase);
        }

        [Fact]
        [Trait("Amount", "32")]
        public void OrderAsync_Should_Not_Complete_Purchase_Not_Enough_Cash() {
            //Arrange
            Patrimony patrimony = new Patrimony(new Investor(new User()), 10);
            //Act
            var validPurchase = patrimony.IsThereBalanceForPurchase(32);
            // Assert
            Assert.False(validPurchase);
        }
    }
}
