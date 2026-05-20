using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketPriorityRepository : BaseRepository<TicketPriority>, ITicketPriorityRepository
{
  public TicketPriorityRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<TicketPriority?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<TicketPriority>()
                 .FirstOrDefaultAsync(ticketPriority => ticketPriority.Name == name, cancellationToken);
  }
}
