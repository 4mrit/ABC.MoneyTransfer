using ABC.MoneyTransfer.Core.Entities;
using ABC.MoneyTransfer.Core.Interfaces;
using ABC.MoneyTransfer.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

public class TransactionRepository : ITransactionRepository
{
    private readonly AppDbContext _context;

    public TransactionRepository(AppDbContext context) { _context = context; }

    public async Task<Transaction?> GetTransactionByIdAsync(Guid id)
    {
        return await _context.Transactions.FindAsync(id);
    }

    public async Task<IEnumerable<Transaction>>
    GetUserTransactionsAsync(Guid userId)
    {
        return await _context.Transactions
            .Where(t => t.SenderId == userId.ToString())
            .ToListAsync();
    }

    public async Task<Transaction?>
    CreateTransactionAsync(Transaction transaction)
    {
        _context.Transactions.Add(transaction);
        await _context.SaveChangesAsync();
        return transaction;
    }
}