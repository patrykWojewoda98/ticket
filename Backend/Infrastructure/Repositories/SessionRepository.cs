using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SessionRepository : BaseRepository<Session>, ISessionRepository
{
  public SessionRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Session>> FindByUserIdAsync(string userId)
  {
    return await _dbSet
                 .Where(session => session.UserId == userId)
                 .ToListAsync();
  }

  public async Task<Session?> FindByTokenAsync(string token)
  {
    return await _dbSet
                 .FirstOrDefaultAsync(session => session.Token == token);
  }
}
