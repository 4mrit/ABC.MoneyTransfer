using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ABC.MoneyTransfer.Core.Entities;

namespace ABC.MoneyTransfer.Infrastructure.Data;
// public class AppDbContext
//     : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> {
//   public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
//   {}

public class AppDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid> {
  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

  public DbSet<Transaction> Transactions { get; set; } = null!;
  public DbSet<ExchangeRate> ExchangeRates { get; set; } = null!;

  protected override void OnModelCreating(ModelBuilder builder) {
    base.OnModelCreating(builder);
    builder.Entity<Transaction>().Property(t => t.TransferAmountMYR);
    builder.Entity<Transaction>().Property(t => t.PayoutAmountNPR);
    builder.Entity<ExchangeRate>().Property(e => e.Rate);
  }
}