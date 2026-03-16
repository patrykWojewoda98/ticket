using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserRepository : IBaseRepository<User>
{
  Task<List<User>> FindByRoleAsync(string role, CancellationToken cancellationToken = default);
  Task<User> SetUserRoleAsync(int userId, string role, CancellationToken cancellationToken = default);
}
