using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class SessionRepository : ISessionRepository
{
  private readonly DatabaseContext _context;

  public SessionRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<Session>> GetAllAsync()
  {
    return await _context.Sessions.ToListAsync();
  }

  public async Task<Session?> GetByIdAsync(string id)
  {
    return await _context.Sessions.FindAsync(id);
  }

  public async Task<IEnumerable<Session>> FindByUserIdAsync(string userId)
  {
    return await _context.Sessions
                         .Where(session => session.UserId == userId)
                         .ToListAsync();
  }

  public async Task<Session?> FindByTokenAsync(string token)
  {
    return await _context.Sessions
                         .FirstOrDefaultAsync(session => session.Token == token);
  }

  public async Task<Session> CreateAsync(Session session)
  {
    await _context.Sessions.AddAsync(session);
    await _context.SaveChangesAsync();
    return session;
  }

  public async Task<Session> UpdateAsync(string id, Session session)
  {
    var existingSession = await _context.Sessions.FindAsync(id);
    if (existingSession != null)
    {
      existingSession.UserId = session.UserId;
      existingSession.Token = session.Token;
      existingSession.ExpiresAt = session.ExpiresAt;
      existingSession.IpAddress = session.IpAddress;
      existingSession.UserAgent = session.UserAgent;
      existingSession.UpdatedAt = session.UpdatedAt;

      _context.Sessions.Update(existingSession);
      await _context.SaveChangesAsync();
      return existingSession;
    }
    return session;
  }

  public async Task DeleteAsync(string id)
  {
    var existingSession = await _context.Sessions.FindAsync(id);
    if (existingSession != null)
    {
      _context.Sessions.Remove(existingSession);
      await _context.SaveChangesAsync();
    }
  }
}
