namespace ABC.MoneyTransfer.Core.Entities;
public class ExchangeRate {
  public int Id { get; set; }
  public string CurrencyFrom { get; set; } = "MYR";
  public string CurrencyTo { get; set; } = "NPR";
  public decimal Rate { get; set; }
  public DateTime RetrievedAt { get; set; } = DateTime.UtcNow;
}