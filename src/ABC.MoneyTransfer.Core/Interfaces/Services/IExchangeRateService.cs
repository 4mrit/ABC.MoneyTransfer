namespace ABC.MoneyTransfer.Core.Interfaces;

using ABC.MoneyTransfer.Core.Common;
using ABC.MoneyTransfer.Core.DTOs;

public interface IExchangeRateService
{
    Task<OperationResult<ExchangeRateResponseDTO>> GetCurrentExchangeRateAsync();
}