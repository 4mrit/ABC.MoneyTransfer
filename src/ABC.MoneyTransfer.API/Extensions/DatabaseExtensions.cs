using ABC.MoneyTransfer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace ABC.MoneyTransfer.API.Extensions;
public static class DatabaseExtension {

  public static async Task ConfigureDatabaseAsync(this WebApplication app) {
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    Console.WriteLine("===================================================");
    Console.WriteLine("Configuring DB");
    Console.WriteLine(dbContext.Database.GetConnectionString());
    await EnsureDatabaseCreated(dbContext);
    await RunMigrationsAsync(dbContext);
  }

  public static async Task EnsureDatabaseCreated(AppDbContext dbContext) {
    var dbCreator = dbContext.GetService<IRelationalDatabaseCreator>();
    var strategy = dbContext.Database.CreateExecutionStrategy();
    await strategy.ExecuteAsync(async () => {
      if (!await dbCreator.ExistsAsync()) {
        Console.WriteLine("Creating Table");
        await dbCreator.CreateAsync();
      }
    });
  }

  public static async Task RunMigrationsAsync(AppDbContext dbContext) {
    var strategy = dbContext.Database.CreateExecutionStrategy();
    await strategy.ExecuteAsync(async () => {
      Console.WriteLine("Updating Migrations");
      await using var transaction =
          await dbContext.Database.BeginTransactionAsync();
      await dbContext.Database.MigrateAsync();
      await transaction.CommitAsync();
    });
  }
}