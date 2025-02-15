using ABC.MoneyTransfer.Core.Entities;

using ABC.MoneyTransfer.Core.DTOs;
namespace ABC.MoneyTransfer.Core.Interfaces;

public interface IExchangeRateRepository
{
    Task<ExchangeRate?> GetLatestExchangeRateAsync();
    Task SaveExchangeRateAsync(ExchangeRate exchangeRate);
}