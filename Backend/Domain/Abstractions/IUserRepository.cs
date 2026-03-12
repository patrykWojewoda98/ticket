using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
  Task<List<User>> FindByRoleAsync(string role);
  Task<User> SetUserRoleAsync(string userId, string role);
}
