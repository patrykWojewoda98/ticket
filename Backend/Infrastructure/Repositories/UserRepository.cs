using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
  private readonly DatabaseContext _context;

  public UserRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<User>> GetAllAsync()
  {
    return await _context.Users.ToListAsync();
  }

  public async Task<User?> GetByIdAsync(string id)
  {
    return await _context.Users.FindAsync(id);
  }

  public async Task<IEnumerable<User>> FindByRoleAsync(string role)
  {
    return await _context.Users
                         .Where(user => user.Role == role)
                         .ToListAsync();
  }

  public async Task<User> SetUserRoleAsync(string userId, string role)
  {
    var existingUser = await _context.Users
        .FirstOrDefaultAsync(user => user.Id == userId);

    if (existingUser != null)
    {
      existingUser.Role = role;
      _context.Users.Update(existingUser);
      await _context.SaveChangesAsync();
      return existingUser;
    }

    return new User { Id = userId, Role = role };
  }

  public async Task<User> CreateAsync(User user)
  {
    await _context.Users.AddAsync(user);
    await _context.SaveChangesAsync();
    return user;
  }

  public async Task<User> UpdateAsync(string id, User user)
  {
    var existingUser = await _context.Users
        .FirstOrDefaultAsync(user => user.Id == id);

    if (existingUser != null)
    {
      existingUser.CompanyId = user.CompanyId;
      existingUser.Email = user.Email;
      existingUser.EmailVerified = user.EmailVerified;
      existingUser.Role = user.Role;
      existingUser.Name = user.Name;
      existingUser.Image = user.Image;
      existingUser.UpdatedAt = user.UpdatedAt;

      _context.Users.Update(existingUser);
      await _context.SaveChangesAsync();
      return existingUser;
    }

    return user;
  }

  public async Task DeleteAsync(string id)
  {
    var existingUser = await _context.Users
        .FirstOrDefaultAsync(user => user.Id == id);

    if (existingUser != null)
    {
      _context.Users.Remove(existingUser);
      await _context.SaveChangesAsync();
    }
  }
}
