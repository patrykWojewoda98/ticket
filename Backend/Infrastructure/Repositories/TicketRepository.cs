using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
  public TicketRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Ticket>> FindByUserIdAsync(int userId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Ticket>()
                 .Where(ticket => ticket.UserId == userId)
                 .ToListAsync(cancellationToken);
  }

  public async Task<List<Ticket>> FindByAssigneeIdAsync(int assigneeId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<Ticket>()
                 .Where(ticket => ticket.AssigneeId == assigneeId)
                 .ToListAsync(cancellationToken);
  }
}
