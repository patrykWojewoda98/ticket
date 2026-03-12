using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : BaseRepository<User>, IUserRepository
{
  public UserRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<User>> FindByRoleAsync(string role)
  {
    return await _dbSet
                 .Where(user => user.Role == role)
                 .ToListAsync();
  }

  public async Task<User> SetUserRoleAsync(string userId, string role)
  {
    var existingUser = await _dbSet
        .FirstOrDefaultAsync(user => user.Id == userId);

    if (existingUser != null)
    {
      existingUser.Role = role;
      _dbSet.Update(existingUser);
      await _dbContext.SaveChangesAsync();
      return existingUser;
    }

    var newUser = new User { Id = userId, Role = role };
    await _dbSet.AddAsync(newUser);
    await _dbContext.SaveChangesAsync();
    return newUser;
  }
}
