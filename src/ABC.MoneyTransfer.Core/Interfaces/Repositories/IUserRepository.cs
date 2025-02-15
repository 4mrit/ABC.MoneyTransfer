using ABC.MoneyTransfer.Core.Entities;

namespace ABC.MoneyTransfer.Core.Interfaces;
public interface IUserRepository {
  Task<ApplicationUser?> GetUserByEmailAsync(string email);
  Task<ApplicationUser?> CreateUserAsync(ApplicationUser user,
                                         string passwordHash);
}