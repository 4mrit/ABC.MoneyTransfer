using ABC.MoneyTransfer.Core.Entities;
namespace ABC.MoneyTransfer.Core.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction?> GetTransactionByIdAsync(Guid id);
    Task<IEnumerable<Transaction>> GetUserTransactionsAsync(Guid userId);
    Task<Transaction?> CreateTransactionAsync(Transaction transaction);
}