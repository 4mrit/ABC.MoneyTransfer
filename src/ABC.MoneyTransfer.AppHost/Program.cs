var builder = DistributedApplication.CreateBuilder(args);

var sqlPassword = builder.AddParameter("sql-password", secret: true);

var sql = builder.AddSqlServer("sqlserver", password: sqlPassword)
              .WithDataVolume()
              .AddDatabase("sqldb");

builder.AddProject<Projects.ABC_MoneyTransfer_API>("apiservice")
    .WithReference(sql);

await builder.Build().RunAsync();