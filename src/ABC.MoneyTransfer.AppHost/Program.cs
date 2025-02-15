var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

var database = builder.AddSqlServer("sqlserver")
                   .WithDataVolume()
                   .WithEnvironment("ACCEPT_EULA", "Y")
                   .WithEnvironment("SA_PASSWORD", "YourStrong!Passw0rd");

var apiService =
    builder.AddProject<Projects.ABC_MoneyTransfer_API>("apiservice")
        .WithReference(database)
        .WithReference(cache);

await builder.Build().RunAsync();