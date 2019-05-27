using System;

namespace CreditSuisse.AccountManagement
{
    public class OperationResult
    {
        public Status Status { get; set; }

        public Guid? TransactionId { get; set; }

        public decimal? NewBalance { get; set; }
    }
}
