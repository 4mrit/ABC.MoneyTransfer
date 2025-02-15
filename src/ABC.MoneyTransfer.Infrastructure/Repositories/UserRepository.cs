using ABC.MoneyTransfer.Core.Entities;
using ABC.MoneyTransfer.Core.Interfaces;
using ABC.MoneyTransfer.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

public class UserRepository : IUserRepository {
  private readonly AppDbContext _context;

  public UserRepository(AppDbContext context) { _context = context; }

  public async Task<ApplicationUser?> GetUserByEmailAsync(string email) {
    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }

  public async Task<ApplicationUser?> CreateUserAsync(ApplicationUser user,
                                                      string passwordHash) {
    user.PasswordHash = passwordHash;
    _context.Users.Add(user);
    await _context.SaveChangesAsync();
    return user;
  }
}