using Microsoft.AspNetCore.Identity;
namespace ABC.MoneyTransfer.Core.Entities;

public class ApplicationUser : IdentityUser {
  public string FirstName { get; set; }
  public string? MiddleName { get; set; }
  public string LastName { get; set; }
  public string? Address { get; set; }
  public Country Country { get; set; }
}