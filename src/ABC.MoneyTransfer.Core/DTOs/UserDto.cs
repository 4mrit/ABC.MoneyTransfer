using System.ComponentModel.DataAnnotations;
using ABC.MoneyTransfer.Core.Entities;

namespace ABC.MoneyTransfer.Core.DTOs;
public class ApplicationUserRegistrationRequestDTO {
  [Required]
  [EmailAddress]
  public string Email { get; set; } = null!;
  [Required]
  public string Password { get; set; } = null!;
  [Required]
  public string FirstName { get; set; } = null!;
  [Required]
  public string LastName { get; set; } = null!;
  [Required]
  public Country Country { get; set; }

  public string? MiddleName { get; set; }

  public string? Address { get; set; }
}

public class ApplicationUserRegistrationResponseDTO {
  public Guid Id { get; set; }
  public string Email { get; set; } = null!;
  public string FirstName { get; set; } = null!;
  public string LastName { get; set; } = null!;
  public Country Country { get; set; }
  public string? MiddleName { get; set; }
  public string? Address { get; set; }
}

public class ApplicationUserLoginDTO {
  [Required]
  public string Email { get; set; } = null!;
  [Required]
  public string Password { get; set; } = null!;
}