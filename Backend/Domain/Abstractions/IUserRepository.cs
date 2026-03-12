using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository
{
  Task<IEnumerable<User>> GetAllAsync();
  Task<User?> GetByIdAsync(string id);
  Task<IEnumerable<User>> FindByRoleAsync(string role);
  Task<User> SetUserRoleAsync(string userId, string role);
  Task<User> CreateAsync(User user);
  Task<User> UpdateAsync(string id, User user);
  Task DeleteAsync(string id);
}
