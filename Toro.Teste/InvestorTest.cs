using Toro.Domain.Entity;
using Xunit;

namespace Toro.Teste {
    public class InvestorTest {

        [Fact]
        [Trait("Cpf", "11111111111")]
        public void OrderAsync_Should_Complete_Deposit_Investor_Account() {
            //Arrange
            var investor = new Investor("11111111111", 300123, 0001);
            //Act
            var isTrue = investor.IsCpfEqual("11111111111");
            // Assert
            Assert.True(isTrue);
        }

        [Fact]
        [Trait("Cpf", "22222222222")]
        public void OrderAsync_Should_Not_Complete_Deposit_Investor_Account() {
            //Arrange
            var investor = new Investor("11111111111", 300123, 0001);
            //Act
            var isTrue = investor.IsCpfEqual("22222222222");
            // Assert
            Assert.False(isTrue);
        }
    }
}
