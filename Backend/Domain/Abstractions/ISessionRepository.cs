using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ISessionRepository : IBaseRepository<Session>
{
  Task<List<Session>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default);
  Task<Session?> FindByTokenAsync(string token, CancellationToken cancellationToken = default);
}
