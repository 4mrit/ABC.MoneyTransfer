using ABC.MoneyTransfer.Core.Entities;
using ABC.MoneyTransfer.Core.Interfaces;
using ABC.MoneyTransfer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class ExchangeRateRepository : IExchangeRateRepository
{
    private readonly AppDbContext _context;

    public ExchangeRateRepository(AppDbContext context) { _context = context; }

    public async Task<ExchangeRate?> GetLatestExchangeRateAsync()
    {
        return await _context.ExchangeRates.OrderByDescending(e => e.Timestamp)
            .FirstOrDefaultAsync();
    }

    public async Task SaveExchangeRateAsync(ExchangeRate exchangeRate)
    {
        _context.ExchangeRates.Add(exchangeRate);
        await _context.SaveChangesAsync();
    }
}