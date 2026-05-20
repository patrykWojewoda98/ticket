using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketNotificationRepository : BaseRepository<TicketNotification>, ITicketNotificationRepository
{
  public TicketNotificationRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<TicketNotification>> FindByTicketIdAsync(int ticketId, CancellationToken cancellationToken = default)
  {
    return await _dbContext.Set<TicketNotification>()
                 .Where(ticketNotification => ticketNotification.TicketId == ticketId)
                 .ToListAsync(cancellationToken);
  }
}
