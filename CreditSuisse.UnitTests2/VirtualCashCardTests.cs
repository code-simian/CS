using CreditSuisse.AccountManagement;
using NUnit.Framework;
using System;

namespace CreditSuisse.UnitTests
{
    public class VirtualCashCardTests
    {
        const string validPin = "1234";

        [Test]
        public void CallingWithdraw_WithValidArguments_AdjustTheBalanceCorrectly()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act
            var result = card.Withdraw(validPin, 50m);

            // Assert
            Assert.AreEqual(Status.Successful, result.Status);
            Assert.AreEqual(50m, result.NewBalance);
            // NOTE: In a real project transaction ID creation would be injectable/mockable so that it can be asserted
        }
    }
}
