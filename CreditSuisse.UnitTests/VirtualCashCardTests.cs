using CreditSuisse.AccountManagement;
using System;
using Xunit;

namespace CreditSuisse.UnitTests
{
    public class UnitTest1
    {
        const string validPin = "1234";

        [Fact]
        public void CallingWithdraw_WithValidArguments_AdjustTheBalanceCorrectly()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act
            var result = card.Withdraw(validPin, 50m);

            // Assert
            Assert.Equal(Status.Successful, result.Status);
            Assert.Equal(50m, result.NewBalance);
            // NOTE: In a real project transaction ID creation would be injectable/mockable so that it can be asserted
        }
    }
}
