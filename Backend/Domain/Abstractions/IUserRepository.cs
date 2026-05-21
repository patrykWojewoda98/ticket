using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
  Task<List<User>> FindByRoleAsync(string role, CancellationToken cancellationToken = default);
  Task<User> SetUserRoleAsync(int userId, string role, CancellationToken cancellationToken = default);
<<<<<<< HEAD
  Task IncrementFailedAttemptsAsync(int userId, CancellationToken cancellationToken = default);
  Task ResetFailedAttemptsAsync(int userId, CancellationToken cancellationToken = default);
  Task BlockUserAsync(int userId, int attempts, string reason, CancellationToken cancellationToken = default);
  Task<List<BlockedUser>> GetBlockedUsersAsync(CancellationToken cancellationToken = default);
  Task<bool> UnblockUserAsync(int userId, int adminId, CancellationToken cancellationToken = default);
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
}
