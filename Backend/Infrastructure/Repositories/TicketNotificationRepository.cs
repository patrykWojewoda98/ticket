using System;
using Domain.Abstractions;
using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class TicketNotificationRepository : ITicketNotificationRepository
{
  private readonly DatabaseContext _context;

  public TicketNotificationRepository(DatabaseContext context)
  {
    _context = context;
  }

  public async Task<IEnumerable<TicketNotification>> GetAllAsync()
  {
    return await _context.TicketNotifications.ToListAsync();
  }

  public async Task<TicketNotification?> GetByIdAsync(string id)
  {
    return await _context.TicketNotifications.FindAsync(id);
  }

  public async Task<IEnumerable<TicketNotification>> FindByTicketIdAsync(string ticketId)
  {
    return await _context.TicketNotifications
                         .Where(ticketNotification => ticketNotification.TicketId == ticketId)
                         .ToListAsync();
  }

  public async Task<TicketNotification> CreateAsync(TicketNotification ticketNotification)
  {
    await _context.TicketNotifications.AddAsync(ticketNotification);
    await _context.SaveChangesAsync();
    return ticketNotification;
  }

  public async Task<TicketNotification> UpdateAsync(string id, TicketNotification ticketNotification)
  {
    var existingTicketNotification = await _context.TicketNotifications
        .FirstOrDefaultAsync(ticketNotification => ticketNotification.Id == id);

    if (existingTicketNotification != null)
    {
      existingTicketNotification.TicketId = ticketNotification.TicketId;
      existingTicketNotification.UserId = ticketNotification.UserId;
      existingTicketNotification.Message = ticketNotification.Message;
      existingTicketNotification.Read = ticketNotification.Read;

      _context.TicketNotifications.Update(existingTicketNotification);
      await _context.SaveChangesAsync();
      return existingTicketNotification;
    }

    return ticketNotification;
  }

  public async Task DeleteAsync(string id)
  {
    var existingTicketNotification = await _context.TicketNotifications
        .FirstOrDefaultAsync(ticketNotification => ticketNotification.Id == id);

    if (existingTicketNotification != null)
    {
      _context.TicketNotifications.Remove(existingTicketNotification);
      await _context.SaveChangesAsync();
    }
  }
}
