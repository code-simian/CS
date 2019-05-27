using CreditSuisse.AccountManagement;
using System;

namespace CreditSuisse.ConsoleRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            void PrintResults(OperationResult res) => Console.WriteLine($"{res.Status} - {res.TransactionId} - {res.NewBalance}");

            var card = new VirtualCashCard("1234", 100m);

            var result = card.Withdraw("1234", 50m);
            PrintResults(result);

            result = card.TopUp(100m);
            PrintResults(result);

            result = card.Withdraw("1111", 100m);
            PrintResults(result);

            result = card.Withdraw("1234", 200m);
            PrintResults(result);

            Console.ReadLine();
        }
    }
}
