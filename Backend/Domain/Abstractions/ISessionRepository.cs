using System;
using Domain.Entities;

namespace Domain.Abstractions;

public interface ISessionRepository
{
  Task<List<Session>> GetAllAsync();
  Task<Session?> GetByIdAsync(string id);
  Task<List<Session>> FindByUserIdAsync(string userId);
  Task<Session?> FindByTokenAsync(string token);
  Task<Session> CreateAsync(Session session);
  Task<Session> UpdateAsync(string id, Session session);
  Task DeleteAsync(string id);
}
