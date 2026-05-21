using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
  public UserRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<User>> FindByRoleAsync(string role, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<User>()
                 .Where(user => user.Role == role)
                 .ToListAsync(cancellationToken);
  }

  public async Task<User> SetUserRoleAsync(int userId, string role, CancellationToken cancellationToken = default)
  {
    var existingUser = await _dbContext.Set<User>()
        .FirstOrDefaultAsync(user => user.Id == userId, cancellationToken);

    if (existingUser != null)
    {
      existingUser.Role = role;
      _dbContext.Set<User>().Update(existingUser);
      await _dbContext.SaveChangesAsync(cancellationToken);
      return existingUser;
    }

    var newUser = new User { Id = userId, Role = role };
    await _dbContext.Set<User>().AddAsync(newUser, cancellationToken);
    await _dbContext.SaveChangesAsync(cancellationToken);
    return newUser;
  }
<<<<<<< HEAD

  public async Task IncrementFailedAttemptsAsync(int userId, CancellationToken cancellationToken = default)
  {
    var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    if (user == null) return;
    user.FailedLoginAttempts += 1;
    if (user.FailedLoginAttempts >= 5)
    {
      user.IsBlocked = true;
      var blocked = new BlockedUser
      {
        UserId = user.Id,
        BlockedAt = DateTime.UtcNow,
        Attempts = user.FailedLoginAttempts,
        Reason = "failed_attempts"
      };
      await _dbContext.Set<BlockedUser>().AddAsync(blocked, cancellationToken);
    }
    _dbContext.Set<User>().Update(user);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }

  public async Task ResetFailedAttemptsAsync(int userId, CancellationToken cancellationToken = default)
  {
    var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    if (user == null) return;
    user.FailedLoginAttempts = 0;
    _dbContext.Set<User>().Update(user);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }

  public async Task BlockUserAsync(int userId, int attempts, string reason, CancellationToken cancellationToken = default)
  {
    var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    if (user == null) return;
    user.IsBlocked = true;
    user.FailedLoginAttempts = attempts;
    var blocked = new BlockedUser
    {
      UserId = user.Id,
      BlockedAt = DateTime.UtcNow,
      Attempts = attempts,
      Reason = reason
    };
    await _dbContext.Set<BlockedUser>().AddAsync(blocked, cancellationToken);
    _dbContext.Set<User>().Update(user);
    await _dbContext.SaveChangesAsync(cancellationToken);
  }

  public async Task<List<BlockedUser>> GetBlockedUsersAsync(CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<BlockedUser>().ToListAsync(cancellationToken);
  }

  public async Task<bool> UnblockUserAsync(int userId, int adminId, CancellationToken cancellationToken = default)
  {
    var user = await _dbContext.Set<User>().FirstOrDefaultAsync(u => u.Id == userId, cancellationToken);
    if (user == null) return false;
    user.IsBlocked = false;
    user.FailedLoginAttempts = 0;
    var blocked = await _dbContext.Set<BlockedUser>().FirstOrDefaultAsync(b => b.UserId == userId && b.UnblockedAt == null, cancellationToken);
    if (blocked != null)
    {
      blocked.UnblockedAt = DateTime.UtcNow;
      blocked.UnblockedByAdminId = adminId;
      _dbContext.Set<BlockedUser>().Update(blocked);
    }
    _dbContext.Set<User>().Update(user);
    await _dbContext.SaveChangesAsync(cancellationToken);
    return true;
  }
=======
>>>>>>> 8bdda2c58a129a22e9d27085a8ac580aa62d740e
}
