using CreditSuisse.AccountManagement;
using System;
using Xunit;

namespace CreditSuisse.UnitTests
{
    public class UnitTest1
    {
        const string validPin = "1234";
        const string invalidPin = "1111";

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

        [Fact]
        public void CallingWithdraw_WithNullPin_Throws_ArgumentNullException()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act / Assert
            Assert.Throws<ArgumentNullException>("pin", () => card.Withdraw(null, 50m));
        }

        [Fact]
        public void CallingWithdraw_WithEmptyStringPin_Throws_ArgumentNullException()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act / Assert
            Assert.Throws<ArgumentNullException>("pin", () => card.Withdraw("", 50m));
        }

        [Fact]
        public void CallingWithdraw_WithAWithdrawalAmountOfzero_Throws_ArgumentException()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act / Assert
            var exception = Assert.Throws<ArgumentException>(() => card.Withdraw(validPin, 0m));
            Assert.Equal("withdrawalAmount must be a positive number", exception.Message);
        }

        [Fact]
        public void CallingWithdraw_WithANegativeWithdrawalAmount_Throws_ArgumentException()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act / Assert
            var exception = Assert.Throws<ArgumentException>(() => card.Withdraw(validPin, -10m));
            Assert.Equal("withdrawalAmount must be a positive number", exception.Message);
        }

        [Fact]
        public void CallingWithdraw_WithAnInvalidPin_ReturnsInvalidPin_OperationResult()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act
            var result = card.Withdraw(invalidPin, 50m);

            // Assert
            Assert.Equal(Status.InvalidPin, result.Status);
        }

        [Fact]
        public void CallingWithdraw_WithAWithdrawalAmountGreaterThanBalance_ReturnsInsufficientFunds_OperationResult()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act
            var result = card.Withdraw(validPin, 150m);

            // Assert
            Assert.Equal(Status.InsufficientFunds, result.Status);
        }

        [Fact]
        public void CallingTopup_WithATopupAmountOfZero_Throws_ArgumentException()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act / Assert
            var exception = Assert.Throws<ArgumentException>(() => card.TopUp(0m));
            Assert.Equal("topupAmount must be a positive number", exception.Message);
        }

        [Fact]
        public void CallingTopup_WithANegativeTopupAmount_Throws_ArgumentException()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act / Assert
            var exception = Assert.Throws<ArgumentException>(() => card.TopUp(-10m));
            Assert.Equal("topupAmount must be a positive number", exception.Message);
        }

        [Fact]
        public void CallingTopup_WithValidData_AdjustTheBalanceCorrectly()
        {
            // Arrange
            var card = new VirtualCashCard(validPin, 100m);

            // Act
            var result = card.TopUp(50m);

            // Assert
            Assert.Equal(Status.Successful, result.Status);
            Assert.Equal(150m, result.NewBalance);
        }
    }
}
