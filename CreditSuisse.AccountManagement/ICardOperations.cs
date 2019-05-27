
namespace CreditSuisse.AccountManagement
{
    public interface ICardOperations
    {
        OperationResult Withdraw(string pin, decimal amount);

        OperationResult TopUp(decimal amount);
    }
}
