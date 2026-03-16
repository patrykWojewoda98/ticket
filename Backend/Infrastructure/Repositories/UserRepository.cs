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
}
