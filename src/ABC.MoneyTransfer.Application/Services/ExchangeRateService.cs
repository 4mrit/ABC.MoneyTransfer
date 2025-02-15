using ABC.MoneyTransfer.Core.Common;
using ABC.MoneyTransfer.Core.Entities;
using ABC.MoneyTransfer.Core.Interfaces;
using ABC.MoneyTransfer.Core.DTOs;
namespace ABC.MoneyTransfer.Application.Services;

public class ExchangeRateService : IExchangeRateService
{
    private readonly IExchangeRateRepository _exchangeRateRepository;
    private readonly HttpClient _httpClient;

    public ExchangeRateService(IExchangeRateRepository exchangeRateRepository,
                               HttpClient httpClient)
    {
        _exchangeRateRepository = exchangeRateRepository;
        _httpClient = httpClient;
    }

    public async Task<OperationResult<ExchangeRateResponseDTO>>
    GetCurrentExchangeRateAsync()
    {
        var response =
            await _httpClient.GetFromJsonAsync<ExchangeRateApiResponseDTO>(
                "https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5");

        if (response is null || response.Data is null || response.Data.Count == 0)
        {
            return new OperationResult<ExchangeRateResponseDTO>
            {
                Success = false,
                Message = "Failed to fetch exchange rates."
            };
        }

        var latestRate = response.Data.First();
        var exchangeRate =
            new ExchangeRate
            {
                Id = Guid.NewGuid(),
                Currency = latestRate.Currency,
                Rate = latestRate.Rate,
                Timestamp = DateTime.UtcNow
            };

        await _exchangeRateRepository.SaveExchangeRateAsync(exchangeRate);

        return new OperationResult<ExchangeRateResponseDTO>
        {
            Success = true,
            Data = new ExchangeRateResponseDTO
            {
                Currency = latestRate.Currency,
                Rate = latestRate.Rate,
                Timestamp = exchangeRate.Timestamp
            }
        };
    }
}