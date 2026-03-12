using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ISessionRepository : IBaseRepository<Session>
{
  Task<List<Session>> FindByUserIdAsync(string userId);
  Task<Session?> FindByTokenAsync(string token);
}
