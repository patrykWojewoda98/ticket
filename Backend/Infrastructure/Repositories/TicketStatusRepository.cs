using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketStatusRepository : BaseRepository<TicketStatus>, ITicketStatusRepository
{
  public TicketStatusRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<TicketStatus?> FindByNameAsync(string name, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<TicketStatus>()
                 .FirstOrDefaultAsync(ticketStatus => ticketStatus.Name == name, cancellationToken);
  }
}
