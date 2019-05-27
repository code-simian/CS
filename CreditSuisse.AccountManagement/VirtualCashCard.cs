using System;

namespace CreditSuisse.AccountManagement
{
    public class VirtualCashCard : ICardOperations
    {
        private readonly Object mutex = new Object();
        private readonly string _pin;
        private decimal _balance;

        public VirtualCashCard(string pin, decimal balance)
        {
            _pin = pin;
            _balance = balance;
        }

        public OperationResult Withdraw(string pin, decimal withdrawalAmount)
        {
            if (string.IsNullOrEmpty(pin))
            {
                throw new ArgumentNullException(nameof(pin));
            }
            if (withdrawalAmount <= 0)
            {
                throw new ArgumentException($"{nameof(withdrawalAmount)} must be a positive number");
            }

            if (_pin != pin)
            {
                return new OperationResult() { Status = Status.InvalidPin };
            }

            lock (mutex)
            {
                if (_balance - withdrawalAmount < 0)
                {
                    return new OperationResult() { Status = Status.InsufficientFunds };
                }

                _balance -= withdrawalAmount;
            }

            return new OperationResult() { Status = Status.Successful, TransactionId = Guid.NewGuid(), NewBalance = _balance };
        }

        public OperationResult TopUp(decimal topupAmount)
        {
            if (topupAmount <= 0)
            {
                throw new ArgumentException($"{nameof(topupAmount)} must be a positive number");
            }

            lock (mutex)
            {
                _balance += topupAmount;
            }

            return new OperationResult() { Status = Status.Successful, TransactionId = Guid.NewGuid(), NewBalance = _balance };
        }
    }
}
