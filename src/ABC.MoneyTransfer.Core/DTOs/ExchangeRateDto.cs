namespace ABC.MoneyTransfer.Core.DTOs;

public class ExchangeRateResponseDTO
{
    public string Currency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public DateTime Timestamp { get; set; }
}

public class ExchangeRateApiResponseDTO
{
    public List<ExchangeRateDTO> Data { get; set; } = new();
}

public class ExchangeRateDTO
{
    public string Currency { get; set; } = string.Empty;
    public decimal Rate { get; set; }
}