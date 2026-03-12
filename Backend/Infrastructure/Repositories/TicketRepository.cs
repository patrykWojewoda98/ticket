using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
{
  public TicketRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<Ticket>> FindByUserIdAsync(string userId)
  {
    return await _dbSet
                 .Where(ticket => ticket.UserId == userId)
                 .ToListAsync();
  }

  public async Task<List<Ticket>> FindByAssigneeIdAsync(string assigneeId)
  {
    return await _dbSet
                 .Where(ticket => ticket.AssigneeId == assigneeId)
                 .ToListAsync();
  }
}
