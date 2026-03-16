using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SessionRepository : BaseRepository<Session>, ISessionRepository
{
  public SessionRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Session>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Session>()
                 .Where(session => session.UserId == userId)
                 .ToListAsync(cancellationToken);
  }

  public async Task<Session?> FindByTokenAsync(string token, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Session>()
                 .FirstOrDefaultAsync(session => session.Token == token, cancellationToken);
  }
}
