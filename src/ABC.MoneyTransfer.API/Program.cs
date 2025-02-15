using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ABC.MoneyTransfer.Infrastructure.Data;
using ABC.MoneyTransfer.Core.Interfaces;
using ABC.MoneyTransfer.Core.Entities;
using ABC.MoneyTransfer.Application.Services;
using ABC.MoneyTransfer.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Services
builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

// Configure Database Connection
var connectionString = builder.Configuration.GetConnectionString("sqldb");
builder.Services.AddDbContextPool<AppDbContext>(
    dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(
        connectionString, o => o.MigrationsAssembly("ABC.MoneyTransfer.API")));
builder.EnrichSqlServerDbContext<AppDbContext>();
builder.AddServiceDefaults();

// Identity Setup
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>(options =>
    {
        options.Password = new PasswordOptions
        {
            RequireDigit = true,
            RequiredLength = 8,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireNonAlphanumeric = true
        };
    })
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Cookie Authentication
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Auth/login";
    options.AccessDeniedPath = "/Auth/AccessDenied";
    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
});

builder.Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

// Register Services
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionService, TransactionService>();
// builder.Services.AddScoped<ITransactionService, TransactionService>();
// builder.Services.AddScoped<IExchangeRateService, ExchangeRateService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    await app.ConfigureDatabaseAsync();
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();