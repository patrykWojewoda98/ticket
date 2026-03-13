using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketNotificationRepository : BaseRepository<TicketNotification>, ITicketNotificationRepository
{
  public TicketNotificationRepository(DatabaseContext databaseContext) : base(databaseContext) { }

  public async Task<List<TicketNotification>> FindByTicketIdAsync(int ticketId)
  {
    return await _dbSet
                 .Where(ticketNotification => ticketNotification.TicketId == ticketId)
                 .ToListAsync();
  }
}
